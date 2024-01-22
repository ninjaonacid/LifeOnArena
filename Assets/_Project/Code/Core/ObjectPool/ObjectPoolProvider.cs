using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Core.ObjectPool
{
    public class ObjectPoolProvider : IInitializable, IDisposable
    {
        private IObjectResolver _resolver;
        private Dictionary<int, ObjectPool<PooledObject>> _identifierToPoolMap;
        private Dictionary<string, ObjectPool<PooledObject>> _prefabToPoolMap;
        
        public ObjectPoolProvider(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public void Initialize()
        {
            _identifierToPoolMap = new Dictionary<int, ObjectPool<PooledObject>>();
            _prefabToPoolMap = new Dictionary<string, ObjectPool<PooledObject>>();
        }

        public GameObject Spawn(int id, GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            ObjectPool<PooledObject> objectPool = null;

            if (!_identifierToPoolMap.TryGetValue(id, out objectPool))
            {
                objectPool = new ObjectPool<PooledObject>(() => 
                    Instantiate(prefab, position, rotation, parent), prefab);
                
                objectPool.Initialize();
            }
      
            return objectPool.Get().gameObject;
        }
        

        public GameObject Spawn(int id, GameObject prefab)
        {
            ObjectPool<PooledObject> objectPool = null;

            if (!_identifierToPoolMap.TryGetValue(id, out objectPool))
            {
                objectPool = new ObjectPool<PooledObject>(() => Instantiate(prefab), prefab);
                objectPool.Initialize();
            }
      
            return objectPool.Get().gameObject;
        }

        public void Release(int id)
        {
            ObjectPool<PooledObject> objectPool = null;
            
            if(_identifierToPoolMap.TryGetValue(id, out objectPool))
            {
                
            }
        }

        public TComponent Spawn<TComponent>(int id, GameObject prefab)
        {
           var go =  Spawn(id, prefab);
           return go.GetComponent<TComponent>();
        }
        
        private PooledObject Instantiate(GameObject prefab)  
        {
            var go = _resolver.Instantiate(prefab);
            return go.GetComponent<PooledObject>();
        }
        
        private PooledObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)   
        {
            var go = _resolver.Instantiate(prefab, position, rotation, parent);
            return go.GetComponent<PooledObject>();
        }


        public void Dispose()
        {
            
        }
    }
}