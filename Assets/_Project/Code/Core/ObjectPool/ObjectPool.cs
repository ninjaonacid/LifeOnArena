using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.ObjectPool
{
    public class ObjectPool<T> where T : Component, IPoolable
    {
        private GameObject _poolRoot;

        private GameObject _prefab;
        private readonly Func<T> _factory;
        private readonly Stack<T> _objectsStock;

        public ObjectPool(Func<T> factory, GameObject prefab)
        {
            _factory = factory;
            _prefab = prefab;
            _objectsStock = new Stack<T>();
        }

        public void Initialize()
        {
            if (_poolRoot is null)
            {
                _poolRoot = new GameObject($"{_prefab.gameObject.name} Pool");
            }
        }
        public T Get(Transform parent = null)
        {
            T obj = default;
            
            if (_objectsStock.Count > 0)
            {
                obj = _objectsStock.Pop();
            }
            else
            {
                obj = CreateObject();
            }

            if (!parent)
            {
                obj.transform.SetParent(_poolRoot.transform);
            }
            
            obj.gameObject.SetActive(true);

            return obj;
        }

        private T CreateObject() 
        {
            var obj = _factory();

            obj.Initialize(Return);

            return obj;
        }

        public void Release()
        {
            
        }

        private void Return(PooledObject obj)
        {
            _objectsStock.Push(obj as T);
            obj.gameObject.SetActive(false);
        }

    }
}
