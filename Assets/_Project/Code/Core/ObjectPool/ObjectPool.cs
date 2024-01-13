using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Code.Core.ObjectPool
{
    public class ObjectPool
    {
        private IObjectResolver _resolver;
        private GameObject _pooledObject;
        private GameObject _poolRoot;

        private Queue<GameObject> _objectsStock;
        public ObjectPool(IObjectResolver resolver, GameObject pooledObject)
        {
            _resolver = resolver;
            _pooledObject = pooledObject;
        }
        
        public void Initialize()
        {
            _poolRoot = new GameObject($"{_pooledObject.name} pool");
            Object.DontDestroyOnLoad(_poolRoot);
        }

        public GameObject Spawn(Transform parent)
        {
           //if(_pooledObject.)
           return null;
        }
    }
}
