using System;
using Code.Logic.Collision;
using UnityEngine;

namespace Code.Logic.Weapon
{
    [RequireComponent(typeof(Collider))]
    public class MeleeWeapon : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private ParticleSystem _slashTrail;
        public event Action<CollisionData> Hit;
        private LayerMask _mask;

        private void Awake()
        {
            _mask = LayerMask.NameToLayer("Hittable");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_mask.value == other.gameObject.layer)
            {
                Hit?.Invoke(new CollisionData()
                {
                    Target = other.gameObject
                });
            }
        }

        public void EnableCollider(bool value)
        {
            _collider.enabled = value;
        }

        public void EnableTrail(bool value)
        {
            var slashTrailEmission = _slashTrail.emission;
            slashTrailEmission.enabled = value;
        }
    }
}
