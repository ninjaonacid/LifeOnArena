﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Core.ObjectPool
{
    public class ObjectPoolProvider : IDisposable
    {
        private readonly Dictionary<int, ObjectPool<PooledObject>> _identifierToPoolMap;
        private readonly Dictionary<int, ObjectPool<PooledObject>> _prefabToPoolMap;

        public ObjectPoolProvider()
        {
            _identifierToPoolMap = new Dictionary<int, ObjectPool<PooledObject>>();
            _prefabToPoolMap = new Dictionary<int, ObjectPool<PooledObject>>();
        }

        public GameObject Pull(int id, GameObject prefab, Transform parent)
        {
            if (!_identifierToPoolMap.TryGetValue(id, out var objectPool))
            {
                objectPool = CreatePool(prefab);
                
                _identifierToPoolMap.Add(id, objectPool);
                
                objectPool.Initialize();
            }
      
            return objectPool.Get(parent).gameObject;
        }
        

        public GameObject Pull(int id, GameObject prefab)
        {
            if (!_identifierToPoolMap.TryGetValue(id, out var objectPool))
            {
                objectPool = CreatePool(prefab);
                
                _identifierToPoolMap.Add(id, objectPool);
                
                objectPool.Initialize();
            }
      
            return objectPool.Get().gameObject;
        }

        public GameObject Pull(GameObject prefab)
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

        private ObjectPool<PooledObject> CreatePool(GameObject prefab)
        {
            return new ObjectPool<PooledObject>(() => Instantiate(prefab), prefab);
        }

        public void Release(int id)
        {
            ObjectPool<PooledObject> objectPool = null;
            
            if(_identifierToPoolMap.TryGetValue(id, out objectPool))
            {
                
            }
        }

        public TComponent Pull<TComponent>(int id, GameObject prefab)
        {
           var go =  Pull(id, prefab);
           return go.GetComponent<TComponent>();
        }
        
        private PooledObject Instantiate(GameObject prefab)  
        {
            var go = Object.Instantiate(prefab);
            return go.GetComponent<PooledObject>();
        }
        
        private PooledObject Instantiate(GameObject prefab, Transform parent)   
        {
            var go = Object.Instantiate(prefab, parent);
            return go.GetComponent<PooledObject>();
        }


        public void Dispose()
        {
            
        }
    }
}