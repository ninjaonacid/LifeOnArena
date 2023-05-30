using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.EventSystem;
using Code.Infrastructure.SceneManagement;
using Code.Infrastructure.States;
using Code.Services;
using Code.Services.AudioService;
using Code.Services.Input;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using Code.Services.SaveLoad;
using Code.StaticData;
using Code.UI.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Scopes
{
    public class CoreScope : LifetimeScope
    {

        [SerializeField] private LoadingScreen Screen;
        protected override void Configure(IContainerBuilder builder)
        {
            InstallStateMachine(builder);
            
            builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
            builder.Register<IWindowService, WindowsService>(Lifetime.Singleton);
            builder.Register<ILevelEventHandler, LevelEventHandler>(Lifetime.Singleton);
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
            builder.Register<IProgressService,ProgressService>(Lifetime.Singleton);
            builder.Register<IRandomService, RandomService>(Lifetime.Singleton);
            builder.Register<IEventSystem, GameEventSystem>(Lifetime.Singleton);
            builder.Register<IAudioService, AudioService>(Lifetime.Singleton);
            builder.Register<SceneLoader>(Lifetime.Singleton);

            builder.RegisterComponentInNewPrefab(Screen, Lifetime.Singleton).AsImplementedInterfaces();

            InstallInput(builder);
        }

        private void InstallInput(IContainerBuilder builder)
        {
            var inputInstance = GetInput();
            builder.RegisterInstance(inputInstance);
        }
        private  IInputService GetInput()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            return new MobileInputService();
        }

        private void InstallStateMachine(IContainerBuilder builder)
        {
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
            builder.Register<IExitableState, BootstrapGameState>(Lifetime.Singleton);
            builder.Register<IExitableState, GameLoopGameState>(Lifetime.Singleton);
            builder.Register<IExitableState, LoadLevelState>(Lifetime.Singleton);
            builder.Register<IExitableState, MainMenuGameState>(Lifetime.Singleton);
            builder.Register<IExitableState, LoadProgressGameState>(Lifetime.Singleton);
        }
    }
}
