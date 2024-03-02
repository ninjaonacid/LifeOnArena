using System;

namespace Code.Runtime.Core.ObjectPool
{
    public interface IPoolable
    {
        public void InitializePoolable(Action<PooledObject> returnToPool);
        public void ReturnToPool();
    }
}
