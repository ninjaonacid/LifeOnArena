using System;
using Code.ConfigData;
using Code.ConfigData.Identifiers;
using UnityEngine;

namespace Code.Entity
{
    public class EntityWeapon : MonoBehaviour
    {
        [SerializeField] protected WeaponSlot _weaponSlot = new();
        [SerializeField] protected Transform _weaponPosition;
        private WeaponData EquippedWeapon => _weaponSlot.WeaponData;
        
        [Serializable]
        protected class WeaponSlot
        {
            public WeaponId WeaponId;
            public WeaponData WeaponData;
        }
        public WeaponData GetEquippedWeapon()
        {
            return EquippedWeapon;
        }
    }
}
