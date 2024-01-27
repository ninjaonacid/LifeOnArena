using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.ObjectPool
{
    public class ObjectPoolProvider : IDisposable
    {
        private readonly IObjectResolver _objectResolver;
        private readonly Dictionary<int, ObjectPool<PooledObject>> _identifierToPoolMap;
        private readonly Dictionary<int, ObjectPool<PooledObject>> _prefabToPoolMap;

        public ObjectPoolProvider(IObjectResolver resolver)
        {
            _objectResolver = resolver;
            
            _identifierToPoolMap = new Dictionary<int, ObjectPool<PooledObject>>();
            _prefabToPoolMap = new Dictionary<int, ObjectPool<PooledObject>>();
        }

        public GameObject Spawn(int id, GameObject prefab)
        {
            if (!_identifierToPoolMap.TryGetValue(id, out var objectPool))
            {
                objectPool = CreatePool(prefab);
                
                _identifierToPoolMap.Add(id, objectPool);
                
                objectPool.Initialize();
            }
      
            return objectPool.Get().gameObject;
        }

        public GameObject Spawn(GameObject prefab)
        {
            int prefabInstanceId = prefab.GetInstanceID();
            if (!_prefabToPoolMap.TryGetValue(prefabInstanceId, out var objectPool))
            {
                objectPool = CreatePool(prefab);
                
                _prefabToPoolMap.Add(prefabInstanceId, objectPool);
                
                objectPool.Initialize();
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
                
                objectPool.Initialize();
                objectPool.Warm(size);
            }
            else
            {
                objectPool.Warm(size);
            }
        }

        public void WarmPool(int id, GameObject prefab, int size)
        {
            if (!_identifierToPoolMap.TryGetValue(id, out var objectPool))
            {
                objectPool = CreatePool(prefab);
                
                _identifierToPoolMap.Add(id, objectPool);
                
                objectPool.Initialize();
                objectPool.Warm(size);
            }
            else
            {
                objectPool.Warm(size);
            }
        }

        public async UniTaskVoid ReturnWithTimer(int id, PooledObject obj, float time)
        {
            if(_identifierToPoolMap.TryGetValue(id, out var objectPool))
            {
                await UniTask.Delay(TimeSpan.FromSeconds(time));
                objectPool.Return(obj);
            }
            else
            {
                Debug.Log("NO OBJECT IN POOL");
            }
        }

        private ObjectPool<PooledObject> CreatePool(GameObject prefab)
        {
            return new ObjectPool<PooledObject>(() => Instantiate(prefab), prefab);
        }

        public TComponent Spawn<TComponent>(int id, GameObject prefab)
        {
           var go =  Spawn(id, prefab);
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
            
        }
    }
}