using System;
using UnityEngine;

namespace Code.Runtime.Core.ObjectPool
{
    public class PooledObject : MonoBehaviour, IPoolable
    {
        private Action<PooledObject> _returnToPool;
        
        public void InitializePoolable(Action<PooledObject> returnToPool)
        {
            _returnToPool = returnToPool;
        }

        public void ReturnToPool()
        {
           _returnToPool?.Invoke(this);
        }
    }
}