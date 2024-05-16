using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Core.ObjectPool
{
    public class ObjectPoolProvider : IDisposable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly Dictionary<int, ObjectPool<PooledObject>> _prefabToPoolMap;

        public ObjectPoolProvider(IObjectResolver resolver)
        {
            _objectResolver = resolver;

            _prefabToPoolMap = new Dictionary<int, ObjectPool<PooledObject>>();
        }

        public GameObject Spawn(GameObject prefab,
            Action<PooledObject> onCreate = null,
            Action<PooledObject> onRelease = null,
            Action<PooledObject> onGet = null,
            Action<PooledObject> onReturn = null)
        {
            int prefabInstanceId = prefab.GetInstanceID();

            if (!_prefabToPoolMap.TryGetValue(prefabInstanceId, out var objectPool))
            {
                objectPool = CreatePool(prefab, onCreate, onRelease, onGet, onReturn);

                _prefabToPoolMap.Add(prefabInstanceId, objectPool);

                objectPool.Initialize(prefab.name);
            }

            return objectPool.Get().gameObject;
        }

        public void WarmPool(GameObject prefab, int size)
        {
            int prefabInstanceId = prefab.GetInstanceID();

            if (!_prefabToPoolMap.TryGetValue(prefabInstanceId, out var objectPool))
            {
                objectPool = CreatePool(prefab);

                _prefabToPoolMap.Add(prefabInstanceId, objectPool);

                objectPool.Initialize(prefab.name);
                objectPool.Warm(size);
            }
            else
            {
                objectPool.Warm(size);
            }
        }

        private ObjectPool<PooledObject> CreatePool(GameObject prefab, Action<PooledObject> onCreate = null,
            Action<PooledObject> onRelease = null, Action<PooledObject> onGet = null,
            Action<PooledObject> onReturn = null)
        {
            return new ObjectPool<PooledObject>(() => Instantiate(prefab), onCreate, onRelease, onGet, onReturn);
        }

        public TComponent Spawn<TComponent>(GameObject prefab, 
            Action<PooledObject> onCreate = null,
            Action<PooledObject> onRelease = null, Action<PooledObject> onGet = null,
            Action<PooledObject> onReturn = null)
        {
            var go = Spawn(prefab, onCreate, onRelease, onGet, onReturn);
            return go.GetComponent<TComponent>();
        }

        private PooledObject Instantiate(GameObject prefab)
        {
            var go = _objectResolver.Instantiate(prefab);
            return go.GetComponent<PooledObject>();
        }

        private PooledObject Instantiate(GameObject prefab, Transform parent)
        {
            var go = _objectResolver.Instantiate(prefab, parent);
            return go.GetComponent<PooledObject>();
        }


        public void Dispose()
        {
            foreach (var pool in _prefabToPoolMap.Values)
            {
                pool.ReleaseAll();
            }
        }
    }
}