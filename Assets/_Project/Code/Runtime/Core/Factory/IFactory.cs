namespace Code.Runtime.Core.Factory
{
    public interface IFactory<T>
    {
        T Create();
    }

    public interface IFactory<out T, in T1, in T2>
    {
        T Create(T1 t1, T2 t2);
    }

    
}