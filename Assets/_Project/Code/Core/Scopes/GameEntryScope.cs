using Code.Core.EntryPoints.GameEntry;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Scopes
{
    public class GameEntryScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntryPoint>();

            builder.Register<GameStateInitialize>(Lifetime.Scoped);
        }
    }
}
