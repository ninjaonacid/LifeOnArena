using Code.Hero;
using Code.Infrastructure.AssetManagment;
using Code.Infrastructure.EventSystem;
using Code.Infrastructure.Factory;
using Code.Infrastructure.ObjectPool;
using Code.Infrastructure.Services;
using Code.Infrastructure.States;
using Code.Logic;
using Code.Services;
using Code.Services.AudioService;
using Code.Services.Input;
using Code.Services.LevelTransitionService;
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

        [SerializeField] private LoadingCurtain _curtain;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameBootstrapper>();
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
            builder.Register<IExitableState, BootstrapState>(Lifetime.Singleton);
            builder.Register<IExitableState, GameLoopState>(Lifetime.Singleton);
            builder.Register<IExitableState, LoadLevelState>(Lifetime.Singleton);
            builder.Register<IExitableState, MainMenuState>(Lifetime.Singleton);
            builder.Register<IExitableState, LoadProgressState>(Lifetime.Singleton);
            builder.Register<IEnemyFactory, EnemyFactory>(Lifetime.Singleton);
            builder.Register<IHeroFactory, HeroFactory>(Lifetime.Singleton);
            builder.Register<IAbilityFactory, AbilityFactory>(Lifetime.Singleton);
            builder.Register<IItemFactory, ItemFactory>(Lifetime.Singleton);
            builder.Register<IStaticDataService, StaticDataService>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
            builder.Register<IWindowService, WindowsService>(Lifetime.Singleton);
            builder.Register<ILevelEventHandler, LevelEventHandler>(Lifetime.Singleton);
            builder.Register<IAssetsProvider, AssetProvider>(Lifetime.Singleton);
            builder.Register<IProgressService,ProgressService>(Lifetime.Singleton);
            builder.Register<IRandomService, RandomService>(Lifetime.Singleton);
            builder.Register<IEnemyObjectPool, EnemyObjectPool>(Lifetime.Singleton);
            builder.Register<IParticleObjectPool, ParticleObjectPool>(Lifetime.Singleton);
            builder.Register<ILevelTransitionService, LevelTransitionService>(Lifetime.Singleton);
            builder.Register<IEventSystem, GameEventSystem>(Lifetime.Singleton);
            builder.Register<IAudioService, AudioService>(Lifetime.Singleton);
            builder.Register<SceneLoader>(Lifetime.Singleton);
            
            builder.RegisterComponentInNewPrefab(_curtain, Lifetime.Singleton);
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
    }
}
