using System;
using System.Collections.Generic;
using Code.Core.Factory;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Code.Core.ObjectPool
{
    public class ObjectPoolProvider : IInitializable
    {
        private IObjectResolver _resolver;
        private Dictionary<int, ObjectPool<PooledObject>> _identifierToPoolMap;
        private Dictionary<string, ObjectPool<PooledObject>> _prefabToPoolMap;

        private IEnemyFactory _enemyFactory;
        private ParticleFactory _particleFactory;
        public ObjectPoolProvider(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public void Initialize()
        {
            _identifierToPoolMap = new Dictionary<int, ObjectPool<PooledObject>>();
            _prefabToPoolMap = new Dictionary<string, ObjectPool<PooledObject>>();
        }

        public GameObject Spawn(int id, GameObject prefab, Vector3 position)
        {
            ObjectPool<PooledObject> objectPool = null;

            if (!_identifierToPoolMap.TryGetValue(id, out objectPool))
            {
                objectPool = new ObjectPool<PooledObject>(() => Instantiate(prefab));
            }

            return objectPool.Get().gameObject;
        }

        public TComponent Spawn<TComponent>(int id, GameObject prefab, Vector3 position)
        {
           var go =  Spawn(id, prefab, position);
           return go.GetComponent<TComponent>();
        }
        
        private PooledObject Instantiate(GameObject prefab)
        {
            var go = _resolver.Instantiate(prefab);
            return go.GetComponent<PooledObject>();
        }

    }
}