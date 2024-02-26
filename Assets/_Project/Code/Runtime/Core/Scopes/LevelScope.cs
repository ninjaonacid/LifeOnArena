using Code.Runtime.Core.EntryPoints;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.Installers;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic.WaveLogic;
using Code.Runtime.Services;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Core.Scopes
{
    public class LevelScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LevelStarterPoint>();
            
            builder.Register<EnemySpawnerController>(Lifetime.Scoped);
            builder.Register<LevelController>(Lifetime.Scoped);
            
            builder.Register<ObjectPoolProvider>(Lifetime.Singleton);
            
            builder.Register<IItemFactory, ItemFactory>(Lifetime.Scoped);
            builder.Register<IEnemyFactory, EnemyFactory>(Lifetime.Scoped);
            builder.Register<IAbilityFactory, AbilityFactory>(Lifetime.Scoped);
            builder.Register<IHeroFactory, HeroFactory>(Lifetime.Scoped);

            IInstaller screenServiceInstaller = new ScreenServiceInstaller();
            screenServiceInstaller.Install(builder);
            
            builder.Register<ParticleFactory>(Lifetime.Scoped);

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
