using System;
using Random = UnityEngine.Random;
namespace Code.Runtime.Utils
{
    public static class Utilities
    {
        public static T GetRandomEnumValue<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(Random.Range(0, values.Length));
        }

        public static T GetRandomElementFromArray<T>(T[] array)
        {
            int randomIndex = Random.Range(0, array.Length);
            return array[randomIndex];
        }
    }
}