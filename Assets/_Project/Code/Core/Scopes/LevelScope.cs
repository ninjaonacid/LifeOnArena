using Code.Core.EntryPoints;
using Code.Core.Factory;
using Code.Core.ObjectPool;
using Code.Logic.WaveLogic;
using Code.Services;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Scopes
{
    public class LevelScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LevelStarterPoint>();
            
            builder.Register<EnemySpawnerController>(Lifetime.Scoped);
            builder.Register<LevelController>(Lifetime.Scoped);

            builder.Register<IItemFactory, ItemFactory>(Lifetime.Scoped);
            builder.Register<EnemyObjectPool>(Lifetime.Scoped);
            builder.Register<IEnemyFactory, EnemyFactory>(Lifetime.Scoped);
   
            builder.Register<IAbilityFactory, AbilityFactory>(Lifetime.Scoped);

            InitializeServices(builder);
        }

        private void InitializeServices(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(container =>
            {
                var enemyFactory = container.Resolve<IEnemyFactory>();
                var itemFactory = container.Resolve<IItemFactory>();

                enemyFactory.InitAssets();
                itemFactory.InitAssets();
            });
        }

    }
}
