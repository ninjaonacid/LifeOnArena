using System;
using UnityEngine;

namespace Code.Entity
{
    public class EntityHitBox : MonoBehaviour
    {
        [SerializeField] protected BoxCollider _hitBoxCollider;

        private float offsetY = 3;
        private float offsetZ = 2;
        public Vector3 GetHitBoxCenter()
        {
            return _hitBoxCollider.center;
        }
        
        public Vector3 StartPoint()
        {
            var colliderCenter = _hitBoxCollider.center;

            var position = transform.position;
            return new Vector3(position.x, colliderCenter.y, position.z);
            
        }
        
         private void OnDrawGizmos()
         {
             Gizmos.color = Color.red;

             Transform direction;
             Gizmos.DrawWireSphere(StartPoint() + ((direction = transform).forward * offsetZ) + (direction.up * offsetY), 2);
         }
    }
}
