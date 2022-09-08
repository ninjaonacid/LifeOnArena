using Code.CameraLogic;
using Code.Hero;
using Code.Infrastructure.Factory;
using Code.Logic;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData;
using Code.UI.HUD;
using Code.UI.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly LoadingCurtain _curtain;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly GameStateMachine _stateMachine;
        private IEnemyFactory _enemyFactory;
        private IHeroFactory _heroFactory;
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private IStaticDataService _staticData;
        private ISaveLoadService _saveLoadService;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader,
            LoadingCurtain curtain, AllServices services, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _services = services;
            _uiFactory = uiFactory;
        }
        public void Enter(string sceneName)
        {
            _progressService = _services.Single<IPersistentProgressService>();
            _enemyFactory = _services.Single<IEnemyFactory>();
            _heroFactory = _services.Single<IHeroFactory>();
            _gameFactory = _services.Single<IGameFactory>();
            _staticData = _services.Single<IStaticDataService>();
            _saveLoadService = _services.Single<ISaveLoadService>();

            _curtain.Show();
            _saveLoadService.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitUiCore();
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }
        private void InformProgressReaders()
        {
            foreach (var progressReader in _saveLoadService.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }
        private void InitUiCore()
        {
            _uiFactory.CreateCore();
        }

        private void InitGameWorld()
        {
            string sceneKey = SceneManager.GetActiveScene().name;

            LevelStaticData levelData = _staticData.ForLevel(sceneKey);

            InitSpawners(levelData);

            var hero = InitHero(levelData);

            InitHud(hero);

            CameraFollow(hero);
        }
            
        private void InitSpawners(LevelStaticData levelData)
        {
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                _enemyFactory.CreateSpawner(
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

        private GameObject InitHero(LevelStaticData levelData)
        {
            return _heroFactory.CreateHero(levelData.HeroInitialPosition);
        }

        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}