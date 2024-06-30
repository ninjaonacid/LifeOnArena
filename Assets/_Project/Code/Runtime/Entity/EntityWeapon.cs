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
        
        [SerializeField] protected WeaponSlot _mainWeaponSlot = new();
        [SerializeField] protected WeaponSlot _offHandWeaponSlot = new();
        [SerializeField] protected Transform _equipJointMain;
        [SerializeField] protected Transform _equipJointOffhand;
        [SerializeField] protected WeaponData _weaponData;

        public WeaponData WeaponData => _weaponData;
        public WeaponView MainWeaponView => _mainWeaponSlot.EquippedWeaponView;
        public WeaponView OffHandWeaponView => _offHandWeaponSlot.EquippedWeaponView;
        
        protected ItemFactory _itemFactory;

        [Serializable]
        protected class WeaponSlot
        {
            public WeaponView EquippedWeaponView;
        }


        [Inject]
        public void Construct(ItemFactory itemFactory)
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
            if (weaponData == null)
            {
                return;
            }

            RemoveCurrentWeapon();

            _weaponData = weaponData;

            if (weaponData.IsDual)
            {
                EquipWeaponInSlot(_weaponData.WeaponView, _equipJointMain);
                EquipWeaponInSlot(_weaponData.WeaponView, _equipJointOffhand);
            }
            else
            {
                EquipWeaponInSlot(_weaponData.WeaponView, _equipJointMain);

                if (weaponData.IsWithShield)
                {
                    EquipWeaponInSlot(_weaponData.ShieldPrefab, _equipJointOffhand);
                }
            }

            EnableCollider(false);
        }

        private void RemoveCurrentWeapon()
        {
            if (_mainWeaponSlot.EquippedWeaponView != null)
            {
                Destroy(_mainWeaponSlot.EquippedWeaponView.gameObject);
                _mainWeaponSlot.EquippedWeaponView = null;
            }

            if (_offHandWeaponSlot.EquippedWeaponView != null)
            {
                Destroy(_offHandWeaponSlot.EquippedWeaponView.gameObject);
                _offHandWeaponSlot.EquippedWeaponView = null;
            }
        }

        private void EquipWeaponInSlot(WeaponView weaponPrefab, Transform equipJoint)
        {
            var weaponView = _itemFactory.CreateWeapon(weaponPrefab, equipJoint, false);
            weaponView.transform.localPosition = Vector3.zero;

            if (equipJoint == _equipJointMain)
            {
                _mainWeaponSlot.EquippedWeaponView = weaponView;
                OnWeaponChange?.Invoke(_mainWeaponSlot.EquippedWeaponView);
            }
            else if (equipJoint == _equipJointOffhand)
            {
                _offHandWeaponSlot.EquippedWeaponView = weaponView;
                OnWeaponChange?.Invoke(_offHandWeaponSlot.EquippedWeaponView);
            }
        }
        
        //CallFromAnimation
        public void EnableWeaponCollider()
        {
            EnableCollider(true);
        }
        
        public void DisableWeaponCollider()
        {
            EnableCollider(false);
        }

        private void EnableCollider(bool value)
        {
            _mainWeaponSlot.EquippedWeaponView.EnableCollider(value);
            
            if(_offHandWeaponSlot.EquippedWeaponView != null)
                _offHandWeaponSlot.EquippedWeaponView.EnableCollider(value);
        }

        public Transform GetEquipJointMainTransform => _equipJointMain;

    }
}
