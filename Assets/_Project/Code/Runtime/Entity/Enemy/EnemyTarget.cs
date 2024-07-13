using Code.Runtime.Entity.Hero;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyTarget : MonoBehaviour
    {
        private Transform _targetTransform;
        private HeroDeath _heroDeath;

        public void Construct(Transform heroTransform)
        {
            _targetTransform = heroTransform;
            _heroDeath = _targetTransform.GetComponent<HeroDeath>();
        }

        public void RotationToTarget()
        {
            if (!HasTarget()) return;
            
            Vector3 targetVector = _targetTransform.position - transform.position;
            targetVector.y = 0;
            transform.rotation = Quaternion.LookRotation(targetVector);
        }
        
        public bool HasTarget() => _targetTransform != null && !_heroDeath.IsDead;

        public Transform GetTargetTransform() => _targetTransform;

    }
}
