using System;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Core.Factory;
using Code.Runtime.Logic.Weapon;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity
{
    public class EntityWeapon : MonoBehaviour
    {
        [SerializeField] protected WeaponSlot _weaponSlot = new();
        [SerializeField] protected Transform _weaponPosition;
        [SerializeField] private bool IsCollisionWeapon;
        [SerializeField] protected WeaponData _weaponData;
        
        protected IItemFactory _itemFactory;

        [Serializable]
        protected class WeaponSlot
        {
            public Weapon EquippedWeapon;
        }
        
        [Inject]
        public void Construct(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;

        }
        
        public virtual void EquipWeapon(WeaponData weaponData)
        {
            if (weaponData == null) return;

            if (_weaponSlot.EquippedWeapon != null)
            {
                Destroy(_weaponSlot.EquippedWeapon.gameObject);
            }
     
            _weaponData = weaponData;

            _weaponSlot.EquippedWeapon = _itemFactory.CreateWeapon(_weaponData.WeaponPrefab, _weaponPosition);
            _weaponSlot.EquippedWeapon.gameObject.transform.localPosition = Vector3.zero;

            _weaponSlot.EquippedWeapon.gameObject.transform.localRotation = Quaternion.Euler(
                _weaponData.LocalRotation.x,
                _weaponData.LocalRotation.y,
                _weaponData.LocalRotation.z);
            
            EnableWeapon(IsCollisionWeapon);
        }

        private void InitializeWeapon()
        {
           
        }
        
        public void EnableWeapon(bool value)
        {
            _weaponSlot.EquippedWeapon.EnableCollider(value);
            _weaponSlot.EquippedWeapon.EnableTrail(value);
        }

        public Weapon GetEquippedWeapon() => _weaponSlot.EquippedWeapon;
        public WeaponData GetEquippedWeaponData() => _weaponData;
   
    }
}
