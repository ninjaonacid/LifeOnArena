using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.ObjectPool;
using CodeBase.Infrastructure.Services;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.RandomService;
using CodeBase.Services.SaveLoad;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initialize";
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader,
            AllServices services)
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
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle(InputService());

            _services.RegisterSingle<IPersistentProgressService>
                (new PersistentProgressService());

            _services.RegisterSingle<ISaveLoadService>
            (new SaveLoadService(
                _services.Single<IPersistentProgressService>()));

            _services.RegisterSingle<IGameFactory>
            (new GameFactory(
                _services.Single<IAssets>(),
                _services.Single<IPersistentProgressService>(),
                _services.Single<ISaveLoadService>()));

            _services.RegisterSingle<IHeroFactory>(new HeroFactory(
                _services.Single<IAssets>(),
                _services.Single<ISaveLoadService>()));

            _services.RegisterSingle<IEnemyFactory>(new EnemyFactory(
                _services.Single<IHeroFactory>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IAssets>(),
                _services.Single<ISaveLoadService>(),
                _services.Single<IPersistentProgressService>(),
                _services.Single<IRandomService>()));

            _services.RegisterSingle<IObjectPool>(new GameObjectPool
                (_services.Single<IEnemyFactory>()));
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