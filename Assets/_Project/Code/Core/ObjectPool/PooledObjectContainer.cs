namespace Code.Core.ObjectPool
{
    public class PooledObjectContainer<T> 
    {
        public T Object;
        public bool IsFree;

        public void Consume()
        {
            IsFree = false;
        }

        public void Push()
        {
            IsFree = true;
        }
    }
}
