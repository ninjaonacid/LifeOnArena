using UnityEngine;

namespace Code.Runtime.Entity.Enemy
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
            Vector3 targetVector = _heroTransform.position - transform.position;
            targetVector.y = 0;
            transform.rotation = Quaternion.LookRotation(targetVector);
        }
    }
}
