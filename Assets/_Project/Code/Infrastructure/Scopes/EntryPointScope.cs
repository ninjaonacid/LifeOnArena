using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Scopes
{
    public class EntryPointScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameBootstrapper>();
        }
    }
}
