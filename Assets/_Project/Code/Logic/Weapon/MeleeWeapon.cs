using System;
using Code.Logic.Collision;
using UnityEngine;

namespace Code.Logic.Weapon
{
    public class MeleeWeapon : MonoBehaviour
    {
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
    }
}
