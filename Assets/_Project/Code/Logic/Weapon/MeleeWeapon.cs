using System;
using Code.Logic.Collision;
using UnityEngine;

namespace Code.Logic.Weapon
{
    public class MeleeWeapon : MonoBehaviour
    {
        public event Action<CollisionData> Hit; 

        private void OnTriggerEnter(Collider other)
        {
            Hit?.Invoke(new CollisionData()
            {
                Target = other.gameObject
            });
        }
    }
}
