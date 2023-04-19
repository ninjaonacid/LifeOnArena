using Code.Infrastructure.EntryPoints;
using Code.Infrastructure.Factory;
using Code.Infrastructure.ObjectPool;
using Code.Logic.WaveLogic;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Scopes
{
    public class ArenaSceneScope : LifetimeScope
    {

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ArenaStarterPoint>();
            

            builder.Register<IItemFactory, ItemFactory>(Lifetime.Scoped);
            builder.Register<IEnemyObjectPool, EnemyObjectPool>(Lifetime.Scoped);
            builder.Register<IEnemyFactory, EnemyFactory>(Lifetime.Scoped);
            builder.Register<IHeroFactory, HeroFactory>(Lifetime.Scoped);
            builder.Register<IGameFactory, GameFactory>(Lifetime.Scoped);
            builder.Register<IParticleObjectPool, ParticleObjectPool>(Lifetime.Scoped);
            builder.Register<IAbilityFactory, AbilityFactory>(Lifetime.Scoped);

            builder.Register<WaveController>(Lifetime.Scoped);

        }

    }
}
