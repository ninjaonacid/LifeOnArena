using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Core.EntryPoints;
using Code.Runtime.Core.EventSystem;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.Services.BattleService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.RandomService;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Core.Scopes
{
    public class CoreScope : LifetimeScope
    {
        [SerializeField] private LoadingScreen Screen;
        [SerializeField] private GameAudioPlayer GameAudioPlayer;
        [SerializeField] private AudioService AudioService;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<CoreLoader>();

            builder.Register<IConfigProvider, ConfigProvider.ConfigProvider>(Lifetime.Singleton);
            builder.Register<ISaveLoadService, SaveLoadService>(Lifetime.Singleton);

            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
            builder.Register<IGameDataContainer, GameDataContainer>(Lifetime.Singleton);
            builder.Register<IRandomService, RandomService>(Lifetime.Singleton);
            builder.Register<IEventSystem, GameEventSystem>(Lifetime.Singleton);
            builder.Register<BattleService>(Lifetime.Singleton).AsSelf();

            builder.Register<PlayerControls>(Lifetime.Singleton).AsSelf();
            builder.Register<SceneLoader>(Lifetime.Singleton);

            builder.RegisterComponentInNewPrefab(AudioService, Lifetime.Singleton).DontDestroyOnLoad().AsSelf();
            builder.RegisterComponentInNewPrefab(Screen, Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}