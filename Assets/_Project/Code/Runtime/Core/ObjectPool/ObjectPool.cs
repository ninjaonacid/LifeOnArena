using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Core.ObjectPool
{
    public class ObjectPool<T> where T : Component, IPoolable
    {
        private GameObject _poolRoot;

        private readonly Func<T> _factory;
        private readonly Stack<T> _objectsStock;

        [CanBeNull] private readonly Action<T> _onCreate;
        [CanBeNull] private readonly Action<T> _onRelease;
        [CanBeNull] private readonly Action<T> _onGet;
        [CanBeNull] private readonly Action<T> _onReturn;


        public ObjectPool(Func<T> factory, Action<T> onCreate = null,
            Action<T> onRelease = null,
            Action<T> onGet = null,
            Action<T> onReturn = null)
        {
            _factory = factory;
            _onCreate = onCreate;
            _onGet = onGet;
            _onReturn = onReturn;
            _objectsStock = new Stack<T>();
        }

        public ObjectPool(Func<T> factory)
        {
            _factory = factory;
            _objectsStock = new Stack<T>();
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

            _onGet?.Invoke(obj);

            return obj;
        }

        public void ReleaseAll()
        {
            foreach (var obj in _objectsStock)
            {
                _onRelease?.Invoke(obj);
            }

            _objectsStock.Clear();
        }

        private T CreateObject()
        {
            var obj = _factory();

            obj.InitializePoolable(Return);

            obj.transform.SetParent(_poolRoot.transform);

            _onCreate?.Invoke(obj);

            return obj;
        }


        public void Return(PooledObject obj)
        {
            _objectsStock.Push(obj as T);
            obj.gameObject.SetActive(false);
        }
    }
}