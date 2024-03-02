using System;
using Code.Runtime.Logic.Collision;
using UnityEngine;

namespace Code.Runtime.Logic.Weapon
{
    [RequireComponent(typeof(Collider))]
    public class MeleeWeapon : MonoBehaviour
    {
        public event Action<CollisionData> Hit;

        [SerializeField] private Collider _collider;
        [SerializeField] private ParticleSystem _slashTrail;

        private LayerMask _mask;

        private void Awake()
        {
            _mask = LayerMask.NameToLayer("Hittable");
        }

        private async void OnTriggerEnter(Collider other)
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

            if (value) _slashTrail.Play();
            else _slashTrail.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}