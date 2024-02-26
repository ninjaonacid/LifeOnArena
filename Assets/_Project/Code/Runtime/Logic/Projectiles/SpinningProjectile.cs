using Code.Runtime.Entity.EntitiesComponents;
using UnityEngine;

namespace Code.Runtime.Logic.Projectiles
{
    public class SpinningProjectile : MonoBehaviour
    {
        private float _rotationSpeed = 50f;
        private Transform _rotationPoint;
        private LayerMask _layerMask;
        private void Update()
        {
            ProjectileSpin();
        }

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        public void SetProjectile(Transform rotationPoint)
        {
            _rotationPoint = rotationPoint;
        }
        private void ProjectileSpin()
        {
            transform.RotateAround(
                _rotationPoint.transform.position, 
                new Vector3(0,1,0), 
                _rotationSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if (collision.gameObject.layer == _layerMask)
            {
                if(collision.gameObject.TryGetComponent(out IDamageable health))
                {
                    //health.TakeDamage();
                }
            }
        }
    }
}
