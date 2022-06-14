namespace CodeBase.Services.RandomService
{
    public interface IRandomService : IService
    {
        int RandomizeValue(int min, int max);
    }
}