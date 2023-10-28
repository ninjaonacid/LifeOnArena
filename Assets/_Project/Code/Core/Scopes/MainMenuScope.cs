using Code.Core.EntryPoints;
using Code.Core.Factory;
using Code.Core.Installers;
using Code.Core.ObjectPool;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Scopes
{
    public class MainMenuScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MainMenuStarter>();
            
            
            builder.Register<IItemFactory, ItemFactory>(Lifetime.Scoped);
            builder.Register<IAbilityFactory, AbilityFactory>(Lifetime.Scoped);
            builder.Register<IHeroFactory, HeroFactory>(Lifetime.Scoped);
            
            builder.Register<ParticleFactory>(Lifetime.Scoped);
            builder.Register<ParticleObjectPool>(Lifetime.Scoped);

            IInstaller screenServiceInstaller = new ScreenServiceInstaller();
            screenServiceInstaller.Install(builder);
        }
    }
}
