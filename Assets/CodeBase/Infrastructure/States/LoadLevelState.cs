using CodeBase.CameraLogic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private readonly LoadingCurtain _curtain;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly GameStateMachine _stateMachine;
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticData;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader,
            LoadingCurtain curtain, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _services = services;
        }

        public void Enter(string sceneName)
        {
            _progressService = _services.Single<IPersistentProgressService>();
            _gameFactory = _services.Single<IGameFactory>();
            _staticData = _services.Single<IStaticDataService>();

            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void InitGameWorld()
        {
            InitSpawners();

            var hero = InitHero();

            InitHud(hero);

            CameraFollow(hero);
        }

        private void InitSpawners()
        {
            string sceneKey = SceneManager.GetActiveScene().name;

            LevelStaticData levelData = _staticData.ForLevel(sceneKey);

            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                _gameFactory.CreateSpawner(
                    spawnerData.Position, 
                    spawnerData.Id, 
                    spawnerData.MonsterTypeId);
            }

        }

        private void InitHud(GameObject hero)
        {
            var hud = _gameFactory.CreateHud();

            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        }

        private GameObject InitHero()
        {
            return _gameFactory.CreateHero(GameObject
                .FindGameObjectWithTag(InitialPointTag));
        }

        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}