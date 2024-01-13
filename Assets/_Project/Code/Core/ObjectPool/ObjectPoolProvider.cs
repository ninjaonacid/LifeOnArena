using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Code.Core.ObjectPool
{
    public class ObjectPoolProvider
    {
        private IObjectResolver _resolver;
        private Dictionary<int, ObjectPool> _poolMap;

        public ObjectPoolProvider(IObjectResolver resolver)
        {
            _resolver = resolver;
        }
        
        public GameObject Spawn(GameObject go, Transform parent)
        {
            ObjectPool pool = null;
            
            if (!_poolMap.TryGetValue(go.GetInstanceID(), out pool))
            {
                pool = CreatePool(go);
            }

            GameObject spawnedObject = pool.Spawn(parent);
            
            return null;
        }

        public GameObject Despawn(GameObject go)
        {
            
            return null;
        }

        private ObjectPool CreatePool(GameObject go)
        {
            var pool = new ObjectPool(_resolver, go);
            return pool;
        }
    }
}