using System;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Core.Factory;
using Code.Runtime.Logic.Weapon;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity
{
    public class EntityWeapon : MonoBehaviour
    {
        public event Action<WeaponView> OnWeaponChange;
        
        [SerializeField] protected WeaponSlot _weaponSlot = new();
        [SerializeField] protected Transform _weaponPosition;
        [SerializeField] protected bool IsCollisionWeapon;
        [SerializeField] protected WeaponData _weaponData;
        
        protected IItemFactory _itemFactory;

        [Serializable]
        protected class WeaponSlot
        {
            public WeaponView EquippedWeaponView;
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

        private void Update()
        {
            if (_weaponSlot.EquippedWeaponView)
            {
                _weaponSlot.EquippedWeaponView.transform.rotation = _weaponPosition.rotation;
            }
        }

        public virtual void EquipWeapon(WeaponData weaponData)
        {
            if (weaponData == null) return;

            if (_weaponSlot.EquippedWeaponView != null)
            {
                Destroy(_weaponSlot.EquippedWeaponView.gameObject);
            }
     
            _weaponData = weaponData;

            _weaponSlot.EquippedWeaponView = _itemFactory.CreateWeapon(_weaponData.WeaponView, _weaponPosition);
            _weaponSlot.EquippedWeaponView.gameObject.transform.localPosition = Vector3.zero;

            _weaponSlot.EquippedWeaponView.gameObject.transform.localRotation = Quaternion.Euler(
                _weaponData.LocalRotation.x,
                _weaponData.LocalRotation.y,
                _weaponData.LocalRotation.z);
            
            EnableWeapon(false);
            
            OnWeaponChange?.Invoke(_weaponSlot.EquippedWeaponView);
        }

        public void EnableWeapon(bool value)
        {
            EnableCollider(value);
            EnableTrail(value);
        }

        public void EnableCollider(bool value)
        {
            _weaponSlot.EquippedWeaponView.EnableCollider(value);
        }

        private void EnableTrail(bool value)
        {
            _weaponSlot.EquippedWeaponView.EnableTrail(value);
        }

        public Transform GetEquipJointTransform => _weaponPosition;

        public WeaponView GetEquippedWeapon() => _weaponSlot.EquippedWeaponView;
        public WeaponData GetEquippedWeaponData() => _weaponData;
   
    }
}
