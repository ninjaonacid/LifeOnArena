using System;
using Code.Runtime.Logic.Collision;
using UnityEngine;

namespace Code.Runtime.Logic.Weapon
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class WeaponView : MonoBehaviour
    {
        public event Action<CollisionData> Hit;
        
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _slashTransform;

        public Transform SlashVfxTransform => _slashTransform;

        private LayerMask _targetLayer;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_targetLayer == 1 << other.gameObject.layer)
            {
                Hit?.Invoke(new CollisionData()
                {
                    Target = other.gameObject
                });
            }
        }

        public void SetLayerMask(LayerMask mask)
        {
            _targetLayer = mask;
        }

        public void EnableCollider(bool value)
        {
            _collider.enabled = value;
        }
        
        
        
    }
}