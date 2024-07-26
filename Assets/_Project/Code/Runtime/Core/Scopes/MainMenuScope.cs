using Code.Runtime.Core.EntryPoints;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.Installers;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Modules.RewardSystem;
using Code.Runtime.Modules.TutorialService;
using Code.Runtime.Services;
using Code.Runtime.UI.Services;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Core.Scopes
{
    public class MainMenuScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MainMenuStarter>();

            builder.Register<ProjectileFactory>(Lifetime.Scoped);
            builder.Register<ItemFactory, ItemFactory>(Lifetime.Scoped);
            builder.Register<AbilityFactory, AbilityFactory>(Lifetime.Scoped);
            builder.Register<HeroFactory, HeroFactory>(Lifetime.Scoped);
            builder.Register<UIFactory, UIFactory>(Lifetime.Scoped);
            builder.Register<GameRewardSystem>(Lifetime.Singleton).AsSelf();
            builder.Register<VisualEffectFactory>(Lifetime.Scoped);
            builder.Register<ObjectPoolProvider>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<LevelCollectableTracker>(Lifetime.Scoped);

            IInstaller screenServiceInstaller = new ScreenServiceInstaller();
            screenServiceInstaller.Install(builder);
        }
    }
}
