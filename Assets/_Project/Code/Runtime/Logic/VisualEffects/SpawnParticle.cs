using System;
using Code.Runtime.Core.ObjectPool;
using UnityEngine;

namespace Code.Runtime.Logic.VisualEffects
{
    public class SpawnParticle : MonoBehaviour, IPoolable
    {
        public event Action<PooledObject> ReturnToPool;
   
        public void Initialize(Action<PooledObject> returnToPool)
        {
            ReturnToPool = returnToPool;
        }

        void IPoolable.ReturnToPool()
        {
            ReturnToPool?.Invoke(this.GetComponent<PooledObject>());
        }
    }
    
}
