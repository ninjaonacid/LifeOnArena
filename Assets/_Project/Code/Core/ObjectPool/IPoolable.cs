using System;
using UnityEngine;

namespace Code.Core.ObjectPool
{
    public interface IPoolable
    {
        public event Action Return;
    }
}
