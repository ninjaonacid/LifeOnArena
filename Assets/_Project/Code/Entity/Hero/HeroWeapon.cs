using System;
using Code.ConfigData;
using Code.ConfigData.Identifiers;
using Code.ConfigData.StateMachine;
using Code.Core.Factory;
using Code.Data.PlayerData;
using Code.Logic.Weapon;
using Code.Services.PersistentProgress;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Entity.Hero
{
    public class HeroWeapon : EntityWeapon, ISave
    {
        private MeleeWeapon CurrentWeapon { get; set; }

        [SerializeField] private HeroAnimator _heroAnimator;
        public event Action<MeleeWeapon, WeaponId> OnWeaponChange;

        private IItemFactory _itemFactory;

        [Inject]
        public void Construct(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        public void EquipWeapon(WeaponData weaponData)
        {
            if (weaponData == null) return;

            if (CurrentWeapon != null)
            {
                Destroy(CurrentWeapon.gameObject);
            }
            
            _weaponSlot.WeaponData = weaponData;
            _weaponSlot.WeaponId = weaponData.WeaponId;

            _heroAnimator.OverrideController(weaponData.OverrideController);

            CurrentWeapon = _itemFactory.CreateMeleeWeapon(weaponData.WeaponPrefab, _weaponPosition);
            
            CurrentWeapon.gameObject.transform.localPosition = Vector3.zero;
    
            CurrentWeapon.gameObject.transform.localRotation = Quaternion.Euler(
                weaponData.LocalRotation.x,
                weaponData.LocalRotation.y, 
                weaponData.LocalRotation.z);

            OnWeaponChange?.Invoke(CurrentWeapon, _weaponSlot.WeaponId);
        }

        public void EnableWeapon(bool value)
        {
            CurrentWeapon.EnableCollider(value);
            CurrentWeapon.EnableTrail(value);
        }

        public void LoadData(PlayerData data)
        {
            var weaponId = data.HeroEquipment.WeaponIntId;
            
            if (weaponId != 0)
            {
                WeaponData weapon =  _itemFactory.LoadWeapon(weaponId);
                EquipWeapon(weapon);
            }
            else
            {
                if (_weaponSlot.WeaponData)
                {
                    EquipWeapon(_weaponSlot.WeaponData);
                }
            }
        }
        public void UpdateData(PlayerData data)
        {
            data.HeroEquipment.WeaponStringId = _weaponSlot.WeaponId.Name;
            data.HeroEquipment.WeaponIntId = _weaponSlot.WeaponId.Id;
        }
    }
}
