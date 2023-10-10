using Code.Core.EntryPoints;
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
            
            builder.Register<ViewObjectPool>(Lifetime.Scoped);
          
        }
    }
}
