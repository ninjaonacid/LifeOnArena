using System;
using UnityEngine;

namespace Code.Core.ObjectPool
{
    public class PooledObject : MonoBehaviour, IPoolable
    {
        public void Initialize(Action<PooledObject> returnToPool)
        {
            throw new NotImplementedException();
        }

        void IPoolable.ReturnToPool()
        {
            throw new NotImplementedException();
        }
    }

    
}