
using Random = UnityEngine.Random;

namespace CodeBase.Services.RandomService
{
    public class RandomService : IRandomService
    {
        public int RandomizeValue(int min, int max) =>
            Random.Range(min, max);
        
        
    }
}
