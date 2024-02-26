using System;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;

namespace Code.Runtime.Entity
{
    public class EntityWeapon : MonoBehaviour
    {
        [SerializeField] protected WeaponSlot _weaponSlot = new();
        [SerializeField] protected Transform _weaponPosition;

        [Serializable]
        protected class WeaponSlot
        {
            public WeaponId WeaponId;
            public WeaponData WeaponData;
        }
        
        public WeaponData GetEquippedWeaponData() => _weaponSlot.WeaponData;
        public WeaponId GetEquippedWeaponId() => _weaponSlot.WeaponId;
    }
}
