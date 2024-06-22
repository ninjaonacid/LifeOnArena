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
            if (weaponData == null) return;

            if (weaponData.IsDual)
            {
                if (_mainWeaponSlot.EquippedWeaponView != null)
                {
                    Destroy(_mainWeaponSlot.EquippedWeaponView.gameObject);
                }

                if (_offHandWeaponSlot.EquippedWeaponView != null)
                {
                    Destroy(_offHandWeaponSlot.EquippedWeaponView.gameObject);
                }

                _weaponData = weaponData;
                
                _mainWeaponSlot.EquippedWeaponView = _itemFactory.CreateWeapon(_weaponData.WeaponView, _equipJointMain, false);
                _mainWeaponSlot.EquippedWeaponView.transform.localPosition = Vector3.zero;
                
                _offHandWeaponSlot.EquippedWeaponView = _itemFactory.CreateWeapon(_weaponData.WeaponView, _equipJointOffhand, false);
                _offHandWeaponSlot.EquippedWeaponView.transform.localPosition = Vector3.zero;
            }
            else
            {

                if (_mainWeaponSlot.EquippedWeaponView != null)
                {
                    Destroy(_mainWeaponSlot.EquippedWeaponView.gameObject);
                }

                if (_offHandWeaponSlot.EquippedWeaponView != null)
                {
                    Destroy(_offHandWeaponSlot.EquippedWeaponView.gameObject);
                }

                _weaponData = weaponData;

                _mainWeaponSlot.EquippedWeaponView =
                    _itemFactory.CreateWeapon(_weaponData.WeaponView, _equipJointMain, false);
                _mainWeaponSlot.EquippedWeaponView.transform.localPosition = Vector3.zero;
            }

            EnableCollider(false);
            
            OnWeaponChange?.Invoke(_mainWeaponSlot.EquippedWeaponView);
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
