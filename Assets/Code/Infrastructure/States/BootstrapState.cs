using Code.Infrastructure.AssetManagment;
using Code.Infrastructure.Factory;
using Code.Infrastructure.ObjectPool;
using Code.Infrastructure.Services;
using Code.Services;
using Code.Services.Input;
using Code.Services.LevelTransitionService;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using Code.Services.SaveLoad;
using Code.StaticData;
using Code.UI.Services;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initialize";
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _services;
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader,
            ServiceLocator services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialScene, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            RegisterStaticData();
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IRandomService>(new RandomService());
            RegisterAssetProvider();
            _services.RegisterSingle(InputService());
            _services.RegisterSingle<IWindowService>(
                new WindowsService(_services.Single<IUIFactory>()));
            _services.RegisterSingle<IProgressService>
                (new ProgressService());

            _services.RegisterSingle<ISaveLoadService>
            (new SaveLoadService(
                _services.Single<IProgressService>()));

            _services.RegisterSingle<ILevelEventHandler>(new LevelEventHandler());

            _services.RegisterSingle<IAbilityFactory>(new AbilityFactory(
                _services.Single<IStaticDataService>(),
                _services.Single<IProgressService>(),
                _services.Single<IRandomService>()));

            _services.RegisterSingle<IUIFactory>(new UIFactory
            (_services.Single<IAssetsProvider>(),
                _services.Single<IStaticDataService>(),
                _services.Single<ISaveLoadService>(),
                _services.Single<IProgressService>(),
                _services.Single<IAbilityFactory>()));

            _services.RegisterSingle<IWindowService>(new WindowsService(
                _services.Single<IUIFactory>()));

            _services.RegisterSingle<IParticleObjectPool>(new ParticleObjectPool
                (_services.Single<IStaticDataService>()));

            _services.RegisterSingle<IGameFactory>
            (new GameFactory(
                _services.Single<IAssetsProvider>(),
                _services.Single<IProgressService>(),
                _services.Single<ISaveLoadService>(),
                _services.Single<IWindowService>()));

            _services.RegisterSingle<IHeroFactory>(new HeroFactory(
                _services.Single<IAssetsProvider>(),
                _services.Single<ISaveLoadService>()));

            _services.RegisterSingle<IEnemyFactory>(new EnemyFactory(
                _services.Single<IHeroFactory>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IAssetsProvider>(),
                _services.Single<ISaveLoadService>(),
                _services.Single<IProgressService>(),
                _services.Single<IRandomService>()));


            _services.RegisterSingle<IEnemyObjectPool>(new EnemyObjectPool(
                _services.Single<IEnemyFactory>()));

            _services.RegisterSingle<IItemFactory>(new ItemFactory(
                _services.Single<IStaticDataService>(),
                _services.Single<ISaveLoadService>(),
                _services.Single<IAssetsProvider>(),
                _services.Single<IProgressService>()));

            _services.RegisterSingle<ILevelTransitionService>(new LevelTransitionService(
                _services.Single<IStaticDataService>(), _services.Single<IRandomService>()));
        }

        private void RegisterAssetProvider()
        {
            IAssetsProvider assetsProvider = new AssetProvider();
            assetsProvider.Initialize();

            _services.RegisterSingle<IAssetsProvider>(assetsProvider);

        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
        }

        private IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            return new MobileInputService();
            
            
        }
    }
}