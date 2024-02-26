using Code.Runtime.Core.EntryPoints.GameEntry;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Core.Scopes
{
    public class GameEntryScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntryPoint>();

            builder.Register<InitializeGameState>(Lifetime.Scoped);
        }
    }
}
