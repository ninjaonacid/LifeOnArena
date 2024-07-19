using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

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

        public static Vector3 GetCenterOfCollider(GameObject target)
        {
            if(target.TryGetComponent(out CharacterController controller))
            {
                return controller.center;
            };

            return Vector3.zero;
        }
    }
}