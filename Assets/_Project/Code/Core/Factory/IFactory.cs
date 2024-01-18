namespace Code.Core.Factory
{
    public interface IFactory<T>
    {
        T Create();
    }
}