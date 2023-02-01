using Code.CameraLogic;
using Code.Hero;
using Code.Infrastructure.Factory;
using Code.Infrastructure.ObjectPool;
using Code.Logic;
using Code.Logic.EnemySpawners;
using Code.Logic.LevelObjectsSpawners;
using Code.Logic.LevelTransition;
using Code.Services;
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
        private readonly ServiceLocator _services;
        private readonly GameStateMachine _stateMachine;
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
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader,
            LoadingCurtain curtain, ServiceLocator services, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _services = services;
            _uiFactory = uiFactory;
        }
        public void Enter(string sceneName)
        {
            _inputService = _services.Single<IInputService>();
            _progressService = _services.Single<IProgressService>();
            _enemyFactory = _services.Single<IEnemyFactory>();
            _heroFactory = _services.Single<IHeroFactory>();
            _gameFactory = _services.Single<IGameFactory>();
            _enemyObjectPool = _services.Single<IEnemyObjectPool>();
            _particleObjectPool = _services.Single<IParticleObjectPool>();
            _staticData = _services.Single<IStaticDataService>();
            _saveLoadService = _services.Single<ISaveLoadService>();
            _levelEventHandler = _services.Single<ILevelEventHandler>();
            _abilityFactory = _services.Single<IAbilityFactory>();
            _itemFactory = _services.Single<IItemFactory>();
            _levelTransitionService = _services.Single<ILevelTransitionService>();

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

            LevelConfig levelConfig = _staticData.ForLevel(sceneKey);

            SetupLevelTransitionService(levelConfig);

            SetupEventHandler(levelConfig);

            InitSpawners(levelConfig);

            InitDoors(levelConfig);

            var hero = InitHero(levelConfig);
            InitHud(hero);

            CameraFollow(hero);
        }

        private void InitDoors(LevelConfig levelConfig)
        {
            foreach (var nextLevelDoorData in levelConfig.NextLevelDoorSpawners)
            {
                NextLevelDoor door = _gameFactory.CreateLevelDoor(nextLevelDoorData.Position)
                    .GetComponent<NextLevelDoor>();
                door.transform.rotation = nextLevelDoorData.Rotation;
                door.Construct(_levelEventHandler);

                var levelTrigger = door.GetComponentInChildren<LevelTrigger>();
                levelTrigger.Construct(_stateMachine, _levelTransitionService);
            }
        }

        private void SetupLevelTransitionService(LevelConfig levelConfig)
        {
            _levelTransitionService.SetCurrentLevel(levelConfig);
        }

        private void SetupEventHandler(LevelConfig levelConfig)
        {
            _levelEventHandler.SetCurrentLevel(levelConfig);
            _levelEventHandler.ResetSpawnerCounter();
        }

        private void InitSpawners(LevelConfig levelConfig)
        {
            foreach (EnemySpawnerData spawnerData in levelConfig.EnemySpawners)
            {
                EnemySpawnPoint spawner = _enemyFactory.CreateSpawner(
                    spawnerData.Position,
                    spawnerData.Id,
                    spawnerData.MonsterTypeId,
                    spawnerData.RespawnCount);

                spawner.Construct(_enemyObjectPool, _particleObjectPool, _levelEventHandler);
            }

            foreach (WeaponPlatformSpawnerData weaponPlatform in levelConfig.WeaponPlatformSpawners)
            {
                WeaponPlatformSpawner spawner = _itemFactory.CreateWeaponPlatformSpawner(
                    weaponPlatform.Position,
                    weaponPlatform.Id,
                    weaponPlatform.WeaponId
                );

                spawner.Construct(_itemFactory);
            }
        }

        private GameObject InitHero(LevelConfig levelConfig)
        {
            var hero = _heroFactory.CreateHero(levelConfig.HeroInitialPosition);

            hero.GetComponent<HeroDeath>().Construct(_levelEventHandler);
            hero.GetComponent<HeroSkills>().Construct(_abilityFactory);
            //hero.GetComponent<HeroStateMachine>().Construct(_inputService);
            hero.GetComponent<HeroWeapon>().Construct(_itemFactory);
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