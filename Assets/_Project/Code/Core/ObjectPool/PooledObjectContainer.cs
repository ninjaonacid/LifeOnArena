namespace Code.Core.ObjectPool
{
    public class PooledObjectContainer<T>
    {
        public T Object;
        public IPoolable PoolComponent;
    }
}
