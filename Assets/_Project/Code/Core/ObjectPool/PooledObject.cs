using System;
using UnityEngine;

namespace Code.Core.ObjectPool
{
    public class PooledObject : MonoBehaviour, IPoolable
    {
        public event Action<GameObject> ReturnToPool;
    }
}