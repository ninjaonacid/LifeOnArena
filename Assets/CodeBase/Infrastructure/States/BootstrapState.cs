using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{

    public class BootstrapState : IState
    {
        private const string InitialScene = "Initialize"; 
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

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
 
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }
        public void Exit()
        {
            
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {

            _services.RegisterSingle<IInputService>(InputService());

            _services.RegisterSingle<IAssets>(new AssetProvider());

            _services.RegisterSingle<IPersistentProgressService>
                (new PersistentProgressService());

            _services.RegisterSingle<IGameFactory>
                (new GameFactory(AllServices.Container.Single<IAssets>()));

            _services.RegisterSingle<ISaveLoadService>
                (new SaveLoadService(_services.Single<IPersistentProgressService>(),
                _services.Single<IGameFactory>()));

        }

        private IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
            {
                return  new MobileInputService();
            }
        }
    }

}
