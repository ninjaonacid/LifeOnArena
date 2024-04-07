using System;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Core.Factory;
using Code.Runtime.Logic.Weapon;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;

namespace Code.Runtime.Entity
{
    public class EntityWeapon : MonoBehaviour
    {
        public event Action<Weapon> OnWeaponChange;
        
        [SerializeField] protected WeaponSlot _weaponSlot = new();
        [SerializeField] protected Transform _weaponPosition;
        [SerializeField] protected bool IsCollisionWeapon;
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

        protected virtual void Start()
        {
            if (_weaponData != null)
            {
                EquipWeapon(_weaponData);
            }
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
            
            EnableCollider(IsCollisionWeapon);
        }

        public void EnableWeapon(bool value)
        {
            EnableCollider(value);
            EnableTrail(value);
        }

        protected void EnableCollider(bool value)
        {
            _weaponSlot.EquippedWeapon.EnableCollider(value);
        }

        private void EnableTrail(bool value)
        {
            _weaponSlot.EquippedWeapon.EnableCollider(value);
        }

        protected void WeaponEquipped(Weapon weapon)
        {
            OnWeaponChange?.Invoke(weapon);
        }

        public Weapon GetEquippedWeapon() => _weaponSlot.EquippedWeapon;
        public WeaponData GetEquippedWeaponData() => _weaponData;
   
    }
}
