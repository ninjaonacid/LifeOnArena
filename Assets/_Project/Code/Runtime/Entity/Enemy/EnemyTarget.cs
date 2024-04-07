using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyTarget : MonoBehaviour
    {
        private Transform _targetTransform;

        public void Construct(Transform heroTransform)
        {
            _targetTransform = heroTransform;
        }

        public void RotationToTarget()
        {
            if (!HasTarget()) return;
            
            Vector3 targetVector = _targetTransform.position - transform.position;
            targetVector.y = 0;
            transform.rotation = Quaternion.LookRotation(targetVector);
        }
        
        public bool HasTarget() => _targetTransform != null;
        
    }
}
