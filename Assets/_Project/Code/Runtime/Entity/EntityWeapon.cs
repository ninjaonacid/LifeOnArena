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
        [SerializeField] protected Transform _equipJoint;
        [SerializeField] protected WeaponData _weaponData;

        public WeaponData WeaponData => _weaponData;
        public WeaponView WeaponView => _weaponSlot.EquippedWeaponView;
        
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
        

        public virtual void EquipWeapon(WeaponData weaponData)
        {
            if (weaponData == null) return;

            if (_weaponSlot.EquippedWeaponView != null)
            {
                Destroy(_weaponSlot.EquippedWeaponView.gameObject);
            }
            _weaponData = weaponData;

            _weaponSlot.EquippedWeaponView = _itemFactory.CreateWeapon(_weaponData.WeaponView, _equipJoint, false);
            _weaponSlot.EquippedWeaponView.transform.localPosition = Vector3.zero;

            EnableCollider(false);
            
            OnWeaponChange?.Invoke(_weaponSlot.EquippedWeaponView);
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
            _weaponSlot.EquippedWeaponView.EnableCollider(value);
        }

        public Transform GetEquipJointTransform => _equipJoint;

    }
}
