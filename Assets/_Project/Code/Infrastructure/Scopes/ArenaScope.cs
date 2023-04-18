using Code.Hero;
using Code.Infrastructure.EntryPoints;
using Code.Infrastructure.Factory;
using Code.Infrastructure.ObjectPool;
using Code.Logic.WaveLogic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Scopes
{
    public class ArenaScope : LifetimeScope
    {
        [SerializeField] private HeroHealth _heroHealth;
        [SerializeField] private HeroSkills _heroSkills;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ArenaEntryPoint>();

            builder.Register<IEnemyObjectPool, EnemyObjectPool>(Lifetime.Scoped);
            builder.Register<IEnemyFactory, EnemyFactory>(Lifetime.Scoped);
            builder.Register<IHeroFactory, HeroFactory>(Lifetime.Scoped);
            builder.Register<IGameFactory, GameFactory>(Lifetime.Scoped);
            builder.Register<IParticleObjectPool, ParticleObjectPool>(Lifetime.Scoped);
     
            builder.Register<WaveController>(Lifetime.Scoped);

        }

    }
}
