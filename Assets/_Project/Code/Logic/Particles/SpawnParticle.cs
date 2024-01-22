using System;
using Code.Core.ObjectPool;
using UnityEngine;
using UnityEngine.Pool;

namespace Code.Logic.Particles
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
