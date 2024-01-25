﻿using System;
using UnityEngine;

namespace Code.Core.ObjectPool
{
    public class PooledObject : MonoBehaviour, IPoolable
    {
        private Action<PooledObject> _returnToPool;
        public void Initialize(Action<PooledObject> returnToPool)
        {
            _returnToPool = returnToPool;
        }

        private void OnDisable()
        {
            _returnToPool?.Invoke(this);
        }

        void IPoolable.ReturnToPool()
        {
           _returnToPool?.Invoke(this);
        }
    }

    
}