using System;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Core.Factory;
using Code.Runtime.Logic.Weapon;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity
{
    public class EntityWeapon : MonoBehaviour
    {
        [SerializeField] protected WeaponId _weaponId;
        [SerializeField] protected WeaponSlot _weaponSlot = new();
        [SerializeField] protected Transform _weaponPosition;
        [SerializeField] private bool IsCollisionWeapon;

        protected WeaponData _weaponData;
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

        public void InitializeWeapon()
        {
            if (_weaponId != null)
            {
                _weaponData = _itemFactory.LoadWeapon(_weaponId.Id);
                _weaponSlot.EquippedWeapon = _itemFactory.CreateWeapon(_weaponData.WeaponPrefab, _weaponPosition);
            }
            
            EnableWeapon(IsCollisionWeapon);
        }
        
        public void EnableWeapon(bool value)
        {
            _weaponSlot.EquippedWeapon.EnableCollider(value);
            _weaponSlot.EquippedWeapon.EnableTrail(value);
        }
        
        public WeaponData GetEquippedWeaponData() => _weaponData;
   
    }
}
