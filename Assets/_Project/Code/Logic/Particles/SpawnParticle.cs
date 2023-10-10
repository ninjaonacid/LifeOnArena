using System;
using Code.Core.ObjectPool;
using UnityEngine;

namespace Code.Logic.Particles
{
    public class SpawnParticle : MonoBehaviour, IPoolable
    {
        public event Action<GameObject> ReturnToPool;
    }
    
}
