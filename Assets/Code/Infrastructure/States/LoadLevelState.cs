using System.Collections.Generic;
using Code.CameraLogic;
using Code.Hero;
using Code.Infrastructure.Factory;
using Code.Infrastructure.ObjectPool;
using Code.Logic;
using Code.Logic.EnemySpawners;
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
        private IProgressService _progressService;
        private IStaticDataService _staticData;
        private ISaveLoadService _saveLoadService;
        private IGameEventHandler _gameEventHandler;
        private IEnemyObjectPool _enemyObjectPool;
        private IParticleObjectPool _particleObjectPool;
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
            _progressService = _services.Single<IProgressService>();
            _enemyFactory = _services.Single<IEnemyFactory>();
            _heroFactory = _services.Single<IHeroFactory>();
            _gameFactory = _services.Single<IGameFactory>();
            _enemyObjectPool = _services.Single<IEnemyObjectPool>();
            _particleObjectPool = _services.Single<IParticleObjectPool>();
            _staticData = _services.Single<IStaticDataService>();
            _saveLoadService = _services.Single<ISaveLoadService>();
            _gameEventHandler = _services.Single<IGameEventHandler>();

            _curtain.Show();
            _enemyObjectPool.Cleanup();
            _particleObjectPool.Cleanup();
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

            SetupEventHandler(levelData);

            InitDoors(levelData);

            var hero = InitHero(levelData);

            InitHud(hero);

            CameraFollow(hero);
        }

        private void InitDoors(LevelStaticData levelData)
        {
            NextLevelDoor door = _gameFactory.CreateLevelDoor(levelData.NextLevelDoorPosition)
                .GetComponent<NextLevelDoor>();
            door.Construct(_gameEventHandler);
        }
        private void SetupEventHandler(LevelStaticData levelData)
        {
            _gameEventHandler.LevelSpawnersCount = levelData.EnemySpawners.Count;
            _gameEventHandler.ResetSpawnerCounter();

        }
        private void InitSpawners(LevelStaticData levelData)
        {
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
                SpawnPoint spawner = _enemyFactory.CreateSpawner(
                    spawnerData.Position, 
                    spawnerData.Id, 
                    spawnerData.MonsterTypeId,
                    spawnerData.RespawnCount);

                spawner.Construct(_enemyObjectPool, _particleObjectPool, _gameEventHandler);
            }
        }
        private void InitHud(GameObject hero)
        {
            var hud = _gameFactory.CreateHud();

            hud.GetComponentInChildren<HudSkillContainer>().Construct(_progressService, _staticData);
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        }

        private GameObject InitHero(LevelStaticData levelData)
        {
            var hero =  _heroFactory.CreateHero(levelData.HeroInitialPosition);

            hero.GetComponent<HeroDeath>().Construct(_gameEventHandler);
            return hero;
        }

        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}