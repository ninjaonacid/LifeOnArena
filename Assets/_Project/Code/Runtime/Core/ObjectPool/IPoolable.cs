using System;

namespace Code.Runtime.Core.ObjectPool
{
    public interface IPoolable
    {
        public void Initialize(Action<PooledObject> returnToPool);
        public void ReturnToPool();
    }
}
