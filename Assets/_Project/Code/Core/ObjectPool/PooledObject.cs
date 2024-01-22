﻿using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Code.Core.ObjectPool
{
    public class PooledObject : MonoBehaviour, IPoolable
    {
        private Action<PooledObject> _returnToPool;
        public void Initialize(Action<PooledObject> returnToPool)
        {
            _returnToPool = returnToPool;
        }

        void IPoolable.ReturnToPool()
        {
           _returnToPool.Invoke(this);
        }
    }

    
}