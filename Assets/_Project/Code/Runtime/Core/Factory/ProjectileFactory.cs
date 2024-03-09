using System;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic.Projectiles;

namespace Code.Runtime.Core.Factory
{
    public class ProjectileFactory
    {
        private readonly ObjectPoolProvider _poolProvider;
        private readonly IConfigProvider _configProvider;
        
        public ProjectileFactory(ObjectPoolProvider poolProvider, IConfigProvider configProvider)
        {
            _poolProvider = poolProvider;
            _configProvider = configProvider;
        }

        public Projectile CreateProjectile(int id)
        {
            return null;
        }

        public Projectile CreateProjectile(Projectile prefab)
        {
            var projectile = _poolProvider.Spawn<Projectile>(prefab.gameObject);
            return projectile;
        }

        public Projectile CreateProjectile(Projectile prefab, Action onCreate)
        {
            var projectile = _poolProvider.Spawn<Projectile>(prefab.gameObject);
            return projectile;
        }
    }
}