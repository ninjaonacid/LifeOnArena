using Code.Runtime.Core.EntryPoints;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.Installers;
using Code.Runtime.Core.ObjectPool;
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
            builder.Register<IItemFactory, ItemFactory>(Lifetime.Scoped);
            builder.Register<IAbilityFactory, AbilityFactory>(Lifetime.Scoped);
            builder.Register<IHeroFactory, HeroFactory>(Lifetime.Scoped);
            
            builder.Register<VisualEffectFactory>(Lifetime.Scoped);
            builder.Register<ObjectPoolProvider>(Lifetime.Scoped);

            IInstaller screenServiceInstaller = new ScreenServiceInstaller();
            screenServiceInstaller.Install(builder);
        }
    }
}
