using CodeBase.CameraLogic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.Logic.Inventory;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
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
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _saveLoadService.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void InitGameWorld()
        {
            string sceneKey = SceneManager.GetActiveScene().name;

            LevelStaticData levelData = _staticData.ForLevel(sceneKey);

            InitSpawners(levelData);

            var hero = InitHero(levelData);

            InitHud(hero);

            InitInventoryDisplay(hero);

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

        
        private void InitInventoryDisplay(GameObject hero)
        {
            var inventoryDisplay = _gameFactory.CreateInventoryDisplay();
            var heroInventory = hero.GetComponent<HeroInventory>();

            inventoryDisplay
                .GetComponent<InventoryDisplay>()
                .InventoryItemsView
                .Construct(heroInventory, _gameFactory);
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