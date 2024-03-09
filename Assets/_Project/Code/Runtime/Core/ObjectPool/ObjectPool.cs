using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Core.ObjectPool
{
    public class ObjectPool<T> where T : Component, IPoolable
    {
        private GameObject _poolRoot;
        
        private readonly Func<T> _factory;
        private readonly Stack<T> _objectsStock;

        private readonly Action _onCreate;
        private readonly Action _onRelease;

        public ObjectPool(Func<T> factory, Action onCreate, Action onRelease)
        {
            _factory = factory;
            _onCreate = onCreate;
            _objectsStock = new Stack<T>();
        }

        public ObjectPool(Func<T> factory)
        {
            _factory = factory;
        }

        public void Initialize(string prefabName)
        {
            if (_poolRoot is null)
            {
                _poolRoot = new GameObject($"{prefabName} Pool");
            }
        }

        public void Warm(int size)
        {
            for (int i = 0; i < size; i++)
            {
                var obj = CreateObject();
                Transform transform;
                (transform = obj.transform).SetParent(_poolRoot.transform);
                transform.position = _poolRoot.transform.position;
                obj.gameObject.SetActive(false);
                _objectsStock.Push(obj);
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
            
            obj.gameObject.SetActive(true);

            return obj;
        }

        private T CreateObject() 
        {
            var obj = _factory();

            obj.InitializePoolable(Return);
            
            obj.transform.SetParent(_poolRoot.transform);

            _onCreate?.Invoke();

            return obj;
        }
        

        public void Return(PooledObject obj)
        {
            _objectsStock.Push(obj as T);
            obj.gameObject.SetActive(false);
        }

    }
}
