using UnityEngine;

namespace Code.Enemy
{
    public class EnemyTarget : MonoBehaviour
    {
        private Transform _heroTransform;

        public Transform HeroTransform => _heroTransform;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }

        public void RotationToTarget()
        {
            transform.LookAt(_heroTransform);
        }
    }
}
