using Code.Infrastructure.Factory;
using Code.Infrastructure.Starters;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Scopes
{
    public class MainMenuScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MainMenuStarter>();
            
            builder.Register<IHeroFactory>(Lifetime.Scoped);
        }
    }
}
