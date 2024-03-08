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
        [SerializeField] protected WeaponSlot _weaponSlot = new();
        [SerializeField] protected Transform _weaponPosition;
        protected IItemFactory _itemFactory;
        public Weapon CurrentWeapon { get ; set; }

        [Serializable]
        protected class WeaponSlot
        {
            public WeaponId WeaponId;
            public WeaponData WeaponData;
        }
        
        [Inject]
        public void Construct(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        public void InitializeWeapon()
        {
            if (_weaponSlot.WeaponId != null)
            {
                _weaponSlot.WeaponData = _itemFactory.LoadWeapon(_weaponSlot.WeaponId.Id);
            }
        }
        
        public WeaponData GetEquippedWeaponData() => _weaponSlot.WeaponData;
   
    }
}
