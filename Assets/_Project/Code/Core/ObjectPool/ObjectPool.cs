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
        private Stack<T> _objectsStock;

        public ObjectPool(Func<T> factory, GameObject prefab)
        {
            _factory = factory;
            _prefab = prefab;
        }

        public void Initialize()
        {
            if (_poolRoot is null)
            {
                _poolRoot = new GameObject($"_pooledObject.gameObject.name + Pool");
            }
        }
        public T Get()
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
