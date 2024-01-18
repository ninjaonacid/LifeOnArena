using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace Code.Core.ObjectPool
{
    public class ObjectPool<T>
    {
        private GameObject _poolRoot;

        private readonly Func<T> _factory;
        private Stack<PooledObjectContainer<T>> _objectsStock;
        
        public ObjectPool(Func<T> factory)
        {
            _factory = factory;
        }
        
        public T Get()
        {
            T obj = default;
            
            if (_objectsStock.Count > 0)
            {
                obj = _objectsStock.Pop().Object;
            }
            else
            {
                var container = CreateContainer();
                obj = container.Object;
            }

            return obj;
        }

        private PooledObjectContainer<T> CreateContainer()
        {
            PooledObjectContainer<T> container = new PooledObjectContainer<T>
            {
                Object = _factory(),
            };

            var obj = _factory();


            return container;
        }
        
    }
}
