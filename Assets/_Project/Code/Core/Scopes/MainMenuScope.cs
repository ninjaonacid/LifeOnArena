using Code.Core.EntryPoints;
using Code.Core.Factory;
using Code.Core.ObjectPool;
using Code.Services.BattleService;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Scopes
{
    public class MainMenuScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MainMenuStarter>();
            
          
            builder.Register<IParticleObjectPool, ParticleObjectPool>(Lifetime.Scoped);
        }
    }
}
