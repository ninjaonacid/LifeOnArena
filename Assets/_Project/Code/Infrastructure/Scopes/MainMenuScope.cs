using Code.Infrastructure.Factory;
using Code.Infrastructure.ObjectPool;
using Code.Infrastructure.Starters;
using Code.Services.BattleService;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Scopes
{
    public class MainMenuScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MainMenuStarter>();
            
            builder.Register<IHeroFactory, HeroFactory>(Lifetime.Scoped);
            builder.Register<IItemFactory, ItemFactory>(Lifetime.Scoped);
            builder.Register<IBattleService, BattleService>(Lifetime.Scoped);
            builder.Register<IAbilityFactory, AbilityFactory>(Lifetime.Scoped);
            builder.Register<IParticleObjectPool, ParticleObjectPool>(Lifetime.Scoped);
        }
    }
}
