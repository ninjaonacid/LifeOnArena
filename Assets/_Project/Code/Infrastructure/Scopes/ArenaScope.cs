using Code.Logic.WaveLogic;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Scopes
{
    public class ArenaScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<WaveController>(Lifetime.Singleton);

        }
    }
}
