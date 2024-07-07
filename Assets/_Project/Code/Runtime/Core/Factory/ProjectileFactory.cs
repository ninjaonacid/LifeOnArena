using System;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.ObjectPool;
using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Core.Factory
{
    public class ProjectileFactory
    {
        private readonly ObjectPoolProvider _poolProvider;
        private readonly ConfigProvider _configProvider;

        public ProjectileFactory(ObjectPoolProvider poolProvider, ConfigProvider configProvider)
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

        public Projectile CreateProjectile(Projectile prefab, GameObject owner, Action<Projectile> onCreate, Action<Projectile> onRelease,
            Action<Projectile> onGet, Action<Projectile> onReturn)
        {
            void OnCreateAdapter(PooledObject obj) => onCreate(obj as Projectile);
            void OnReleaseAdapter(PooledObject obj) => onRelease(obj as Projectile);
            void OnGetAdapter(PooledObject obj) => onGet(obj as Projectile);
            void OnReturnAdapter(PooledObject obj) => onReturn(obj as Projectile);

            var projectile = _poolProvider.Spawn<Projectile>(prefab.gameObject, owner, OnCreateAdapter,
                OnReleaseAdapter, OnGetAdapter, OnReturnAdapter);
         
            return projectile;
        }
    }
}