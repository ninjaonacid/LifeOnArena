
using Random = UnityEngine.Random;

namespace Code.Runtime.Services.RandomService
{
    public class RandomService : IRandomService
    {
        public int RandomizeValue(int min, int max) =>
            Random.Range(min, max);

        public int GetRandomNumber(int max) =>
            Random.Range(0, max);
    }
}
