using Code.Runtime.Core.EntryPoints;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.Installers;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic.WaveLogic;
using Code.Runtime.Modules.RewardSystem;
using Code.Runtime.Services;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Core.Scopes
{
    public class LevelScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LevelStarterPoint>();

            builder.Register<LevelController>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<EnemySpawnerController>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<LevelCollectableTracker>(Lifetime.Scoped);
            
            builder.Register<ProjectileFactory>(Lifetime.Scoped);
            builder.Register<GameRewardSystem>(Lifetime.Scoped).AsSelf();
            builder.Register<ObjectPoolProvider>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            
            builder.Register<ItemFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory>(Lifetime.Scoped);
            builder.Register<AbilityFactory>(Lifetime.Scoped);
            builder.Register<HeroFactory>(Lifetime.Scoped);

            builder.Register<LevelScope>(Lifetime.Scoped);

            IInstaller screenServiceInstaller = new ScreenServiceInstaller();
            screenServiceInstaller.Install(builder);
            
            builder.Register<VisualEffectFactory>(Lifetime.Scoped);

            InitializeServices(builder);
        }

        private void InitializeServices(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(container =>
            {
                var enemyFactory = container.Resolve<EnemyFactory>();
                var itemFactory = container.Resolve<ItemFactory>();

                enemyFactory.InitAssets().Forget();
                itemFactory.InitAssets().Forget();
                
            });
        }

    }
}
