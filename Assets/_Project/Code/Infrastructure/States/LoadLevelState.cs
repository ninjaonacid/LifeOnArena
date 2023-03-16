﻿using System.Threading.Tasks;
using Code.CameraLogic;
using Code.Hero;
using Code.Infrastructure.AssetManagment;
using Code.Infrastructure.Factory;
using Code.Infrastructure.ObjectPool;
using Code.Infrastructure.Services;
using Code.Logic;
using Code.Logic.EnemySpawners;
using Code.Logic.LevelObjectsSpawners;
using Code.Logic.LevelTransition;
using Code.Services;
using Code.Services.AudioService;
using Code.Services.Input;
using Code.Services.LevelTransitionService;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData.Levels;
using Code.StaticData.Spawners;
using Code.UI.HUD;
using Code.UI.HUD.Skills;
using Code.UI.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly LoadingCurtain _curtain;
        private readonly SceneLoader _sceneLoader;

        private IEnemyFactory _enemyFactory;
        private IHeroFactory _heroFactory;
        private IGameFactory _gameFactory;
        private IAbilityFactory _abilityFactory;
        private IProgressService _progressService;
        private IInputService _inputService;
        private IStaticDataService _staticData;
        private ISaveLoadService _saveLoadService;
        private ILevelEventHandler _levelEventHandler;
        private IEnemyObjectPool _enemyObjectPool;
        private IParticleObjectPool _particleObjectPool;
        private IItemFactory _itemFactory;
        private ILevelTransitionService _levelTransitionService;
        private IAssetsProvider _assetsProvider;
        private IAudioService _audioService;
        private readonly IUIFactory _uiFactory;

        public IGameStateMachine GameStateMachine { get; set; }

        public LoadLevelState( IInputService input, 
            IHeroFactory heroFactory,
            IEnemyFactory enemyFactory,
            IAbilityFactory abiltiyFactory,
            IProgressService progressService,
            IGameFactory gameFactory,
            IEnemyObjectPool enemyObjectPool,
            IStaticDataService staticData,
            ISaveLoadService saveLoadService,
            ILevelEventHandler levelEventHandler,
            IItemFactory itemFactory,
            IParticleObjectPool particleObjectPool,
            ILevelTransitionService levelTransitionService,
            IAssetsProvider assetsProvider,
            IAudioService audioService,

            SceneLoader sceneLoader,
            LoadingCurtain curtain,IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _uiFactory = uiFactory;
            _heroFactory = heroFactory;
            _enemyFactory = enemyFactory;
            _gameFactory = gameFactory;
            _audioService = audioService;
            _assetsProvider = assetsProvider;
            _enemyObjectPool = enemyObjectPool;
            _staticData = staticData;
            _saveLoadService = saveLoadService;
            _levelEventHandler = levelEventHandler;
            _itemFactory = itemFactory;
            _particleObjectPool = particleObjectPool;
            _levelTransitionService = levelTransitionService;
            _progressService = progressService;

        }

        public void Enter(string sceneName)
        {
            
            _curtain.Show();

            _enemyObjectPool.Cleanup();
            _particleObjectPool.CleanUp();
            _saveLoadService.Cleanup();
            _assetsProvider.Cleanup();

            _enemyFactory.InitAssets();
            _heroFactory.InitAssets();
            _itemFactory.InitAssets();
            _audioService.InitAssets();

            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private async void OnLoaded()
        {
            InitUiCore();
            await InitGameWorld();

            InformProgressReaders();

            GameStateMachine.Enter<GameLoopState>();
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

        private async Task InitGameWorld()
        {
            string sceneKey = SceneManager.GetActiveScene().name;

            LevelConfig levelConfig = _staticData.ForLevel(sceneKey);

            SetupLevelTransitionService(levelConfig);

            SetupEventHandler(levelConfig);

            await InitSpawners(levelConfig);

            InitDoors(levelConfig);

            var hero = await InitHero(levelConfig);

            InitHud(hero);

            CameraFollow(hero);
        }

        private void InitDoors(LevelConfig levelConfig)
        {
            foreach (var nextLevelDoorData in levelConfig.NextLevelDoorSpawners)
            {
                GameObject door = _gameFactory
                    .CreateLevelDoor(nextLevelDoorData.Position, nextLevelDoorData.Rotation);
            }
        }

        private void SetupLevelTransitionService(LevelConfig levelConfig)
        {
            _levelTransitionService.SetCurrentLevel(levelConfig);
        }

        private void SetupEventHandler(LevelConfig levelConfig)
        {
            _levelEventHandler.InitCurrentLevel(levelConfig.EnemySpawners.Count);

        }

        private async Task InitSpawners(LevelConfig levelConfig)
        {
            foreach (EnemySpawnerData spawnerData in levelConfig.EnemySpawners)
            {
                EnemySpawnPoint spawner = await _enemyFactory.CreateSpawner(
                    spawnerData.Position,
                    spawnerData.Id,
                    spawnerData.MonsterTypeId,
                    spawnerData.RespawnCount);

                spawner.Construct(_enemyObjectPool, _particleObjectPool, _levelEventHandler);
            }

            foreach (WeaponPlatformSpawnerData weaponPlatform in levelConfig.WeaponPlatformSpawners)
            {
                WeaponPlatformSpawner spawner = await _itemFactory.CreateWeaponPlatformSpawner(
                    weaponPlatform.Position,
                    weaponPlatform.Id,
                    weaponPlatform.WeaponId
                );

                spawner.Construct(_itemFactory);
            }
        }

      
        private async Task<GameObject> InitHero(LevelConfig levelConfig)
        {
            GameObject hero = await _heroFactory.CreateHero(levelConfig.HeroInitialPosition);

            return hero;
        }

        private void InitHud(GameObject hero)
        {
            var hud = _gameFactory.CreateHud();

            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
            hud.GetComponentInChildren<HudSkillContainer>().Construct(
                hero.GetComponent<HeroSkills>(),
                hero.GetComponent<HeroAbilityCooldown>(),
                _inputService);
        }

        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}