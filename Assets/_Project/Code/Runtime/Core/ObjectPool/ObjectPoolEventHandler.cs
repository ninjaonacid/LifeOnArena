using System;

namespace Code.Runtime.Core.ObjectPool
{
    public class ObjectPoolEventHandler
    {
        public Action<PooledObject> OnCreate;
        public Action<PooledObject> OnRelease;
        public Action<PooledObject> OnGet;
        public Action<PooledObject> OnReturn;
    }
}