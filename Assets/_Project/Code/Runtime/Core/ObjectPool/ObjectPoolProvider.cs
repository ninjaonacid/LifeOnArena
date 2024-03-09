using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
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

        public GameObject Spawn(GameObject prefab)
        {
            int prefabInstanceId = prefab.GetInstanceID();
            if (!_prefabToPoolMap.TryGetValue(prefabInstanceId, out var objectPool))
            {
                objectPool = CreatePool(prefab);
                
                _prefabToPoolMap.Add(prefabInstanceId, objectPool);
                
                objectPool.Initialize(prefab.name);
            }

            return objectPool.Get().gameObject;
        }
        
        public GameObject Spawn(GameObject prefab, Action onCreate, Action onRelease)
        {
            int prefabInstanceId = prefab.GetInstanceID();
            if (!_prefabToPoolMap.TryGetValue(prefabInstanceId, out var objectPool))
            {
                objectPool = CreatePool(prefab, onCreate, onRelease);
                
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
        
        public async UniTaskVoid ReturnWithTimer(PooledObject obj, float time)
        {
            int prefabId = obj.gameObject.GetInstanceID();
            
            if(_prefabToPoolMap.TryGetValue(prefabId, out var objectPool))
            {
                await UniTask.Delay(TimeSpan.FromSeconds(time));
                objectPool.Return(obj);
            }
            else
            {
                Debug.Log("NO OBJECT IN POOL");
            }
        }

        private ObjectPool<PooledObject> CreatePool(GameObject prefab, Action onCreate, Action onRelease)
        {
            return new ObjectPool<PooledObject>(() => Instantiate(prefab), onCreate, onRelease);
        }
        
        private ObjectPool<PooledObject> CreatePool(GameObject prefab)
        {
            return new ObjectPool<PooledObject>(() => Instantiate(prefab));
        }

      
        public TComponent Spawn<TComponent>(GameObject prefab)
        {
            var go =  Spawn(prefab);
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