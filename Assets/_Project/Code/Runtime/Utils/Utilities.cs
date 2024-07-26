using System;
using Code.Runtime.Entity;
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

            if (target.TryGetComponent(out EntityHurtBox hurtBox))
            {
                return hurtBox.GetHitBoxCenter();
            }

            return Vector3.zero;
        }

        public static float GetColliderHeight(GameObject target)
        {
            if (target.TryGetComponent(out CharacterController controller))
            {
                return controller.height;
            }

            if (target.TryGetComponent<EntityHurtBox>(out var hurtBox))
            {
                return hurtBox.GetHeight().y;
            }

            return 0;
        }
    }
}