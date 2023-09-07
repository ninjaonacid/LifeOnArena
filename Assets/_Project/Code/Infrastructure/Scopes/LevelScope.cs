using Code.Infrastructure.Factory;
using Code.Infrastructure.ObjectPool;
using Code.Infrastructure.Starters;
using Code.Logic.WaveLogic;
using Code.Services;
using Code.Services.BattleService;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Scopes
{
    public class LevelScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<AssetInitializer>();
            builder.RegisterEntryPoint<LevelStarterPoint>();
            
            builder.Register<EnemySpawnerController>(Lifetime.Scoped);
            builder.Register<LevelController>(Lifetime.Scoped);

            builder.Register<IItemFactory, ItemFactory>(Lifetime.Scoped);
            builder.Register<IEnemyObjectPool, EnemyObjectPool>(Lifetime.Scoped);
            builder.Register<IEnemyFactory, EnemyFactory>(Lifetime.Scoped);
            builder.Register<IHeroFactory, HeroFactory>(Lifetime.Scoped);
            builder.Register<IGameFactory, GameFactory>(Lifetime.Scoped);
            builder.Register<IParticleObjectPool, ParticleObjectPool>(Lifetime.Scoped);
            builder.Register<IAbilityFactory, AbilityFactory>(Lifetime.Scoped);
            builder.Register<IBattleService, BattleService>(Lifetime.Scoped);
            

        }

    }
}
