using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Code.Core.ObjectPool
{
    public interface IPoolable
    {
        public void Initialize(Action<PooledObject> returnToPool);
        public void ReturnToPool();
    }
}
