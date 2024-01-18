using System;
using System.Collections.Generic;
using Code.Core.Factory;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.ObjectPool
{
    public class ObjectPoolProvider : IInitializable
    {
        private IObjectResolver _resolver;
        private Dictionary<int, ObjectPool<GameObject>> _identifierToPoolMap;
        private Dictionary<string, ObjectPool<GameObject>> _prefabToPoolMap;

        private IEnemyFactory _enemyFactory;
        private ParticleFactory _particleFactory;
        public ObjectPoolProvider(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public void Initialize()
        {
            _identifierToPoolMap = new Dictionary<int, ObjectPool<GameObject>>();
            _prefabToPoolMap = new Dictionary<string, ObjectPool<GameObject>>();
        }

        public GameObject Spawn(int id, GameObject prefab, Vector3 position)
        {
            ObjectPool<GameObject> objectPool = null;

            if (!_identifierToPoolMap.TryGetValue(id, out objectPool))
            {
                objectPool = new ObjectPool<GameObject>(() => Instantiate(prefab));
            }

            return objectPool.Get();
        }

        public TComponent Spawn<TComponent>(int id, GameObject prefab, Vector3 position)
        {
           var go =  Spawn(id, prefab, position);
           return go.GetComponent<TComponent>();
        }

        private GameObject Instantiate(GameObject prefab)
        {
            var go = _resolver.Instantiate(prefab);
            return go;
        }

    }
}