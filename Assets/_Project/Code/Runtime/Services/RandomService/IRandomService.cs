namespace Code.Runtime.Services.RandomService
{
    public interface IRandomService
    {
        int RandomizeValue(int min, int max);
        int GetRandomNumber(int max);
    }
}