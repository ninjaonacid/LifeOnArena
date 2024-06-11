using System;
using UnityEngine;

namespace Code.Runtime.Entity
{
    public class EntityHurtBox : MonoBehaviour
    {
        [SerializeField] protected BoxCollider _hitBoxCollider;
        public Vector3 _initialColliderSize;
        
        

        private float offsetY = 3;
        private float offsetZ = 2;

        private void Start()
        {
            _initialColliderSize = new Vector3(_hitBoxCollider.size.x, _hitBoxCollider.size.y, _hitBoxCollider.size.z);
        }

        public void DisableCollider(bool value)
        {
            if (value) _hitBoxCollider.size = Vector3.zero;
            else
            {
                _hitBoxCollider.size = _initialColliderSize;
            }
        }

        public Vector3 GetHitBoxCenter()
        {
            return _hitBoxCollider.center;
        }

        public Vector3 GetCenterTransform()
        {
            var colliderCenter = _hitBoxCollider.center;

            var position = transform.position;
            
            return new Vector3(position.x, colliderCenter.y, position.z);
        }

        public Vector3 GetHeightTransform()
        {
            var height = _hitBoxCollider.bounds.size;

            var position = transform.position;

            return new Vector3(position.x, height.y - 1, position.z);
        }

        public Vector3 GetHeight()
        {
            var colliderUp = _hitBoxCollider.bounds.size.y;

            return new Vector3(0, colliderUp, 0);
        }
        
         // private void OnDrawGizmos()
         // {
         //     Gizmos.color = Color.red;
         //
         //     Transform direction;
         //     Gizmos.DrawWireSphere(GetCenterTransform() + ((direction = transform).forward * offsetZ) + (direction.up * offsetY), 2);
         // }
    }
}
