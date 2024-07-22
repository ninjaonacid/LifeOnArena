
using Random = UnityEngine.Random;

namespace Code.Runtime.Services.RandomService
{
    public class RandomService
    {
        public int RandomizeValue(int min, int max) =>
            Random.Range(min, max);

        public int GetRandomNumber(int max) => 
            Random.Range(0, max);

        public float RandomizeValue(float min, float max) =>
            Random.Range(min, max);
    }
}
