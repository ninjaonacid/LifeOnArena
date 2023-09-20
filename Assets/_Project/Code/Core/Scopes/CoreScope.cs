using Code.ConfigData.Audio;
using Code.Core.AssetManagement;
using Code.Core.Audio;
using Code.Core.EntryPoints;
using Code.Core.EventSystem;
using Code.Core.SceneManagement;
using Code.Services.AudioService;
using Code.Services.ConfigData;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using Code.Services.SaveLoad;
using Code.UI.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Scopes
{
    public class CoreScope : LifetimeScope
    {

        [SerializeField] private LoadingScreen Screen;
        [SerializeField] private GameAudioPlayer GameAudioPlayer;

        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CoreEntryPoint>();
            
            builder.Register<IConfigProvider, ConfigProvider>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);
            
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
            builder.Register<IScreenModelFactory, ScreenModelFactory>(Lifetime.Singleton);
            builder.Register<IScreenControllerFactory, ScreenControllerFactory>(Lifetime.Singleton);
            builder.Register<IScreenService, ScreenService>(Lifetime.Singleton);
            
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
            builder.Register<IGameDataContainer, GameDataContainer>(Lifetime.Singleton);
            builder.Register<IRandomService, RandomService>(Lifetime.Singleton);
            builder.Register<IEventSystem, GameEventSystem>(Lifetime.Singleton);
            builder.Register<AudioService>(Lifetime.Singleton);

            builder.Register<PlayerControls>(Lifetime.Singleton).AsSelf();
            builder.Register<SceneLoader>(Lifetime.Singleton);

            builder.RegisterComponentInNewPrefab(Screen, Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInNewPrefab(GameAudioPlayer, Lifetime.Singleton).DontDestroyOnLoad();
        }

    }
}
