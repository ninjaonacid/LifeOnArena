using System;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Core.Factory;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Logic.Weapon;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    public class HeroWeapon : EntityWeapon, ISave
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        public event Action<Weapon, WeaponId> OnWeaponChange;
        
        public void EquipWeapon(WeaponData weaponData)
        {
            if (weaponData == null) return;

            if (_weaponSlot.EquippedWeapon != null)
            {
                Destroy(_weaponSlot.EquippedWeapon.gameObject);
            }

            _weaponData = weaponData;
            _weaponId = weaponData.WeaponId;

            _heroAnimator.OverrideController(weaponData.OverrideController);

            _weaponSlot.EquippedWeapon = _itemFactory.CreateWeapon(weaponData.WeaponPrefab, _weaponPosition);

            _weaponSlot.EquippedWeapon.gameObject.transform.localPosition = Vector3.zero;

            _weaponSlot.EquippedWeapon.gameObject.transform.localRotation = Quaternion.Euler(
                weaponData.LocalRotation.x,
                weaponData.LocalRotation.y,
                weaponData.LocalRotation.z);

            OnWeaponChange?.Invoke(_weaponSlot.EquippedWeapon, _weaponId);
        }

        public void LoadData(PlayerData data)
        {
            var weaponId = data.HeroEquipment.WeaponIntId;

            if (weaponId != 0)
            {
                WeaponData weapon = _itemFactory.LoadWeapon(weaponId);
                EquipWeapon(weapon);
            }
            else
            {
                if (_weaponData)
                {
                    WeaponData weapon = _itemFactory.LoadWeapon(_weaponData.WeaponId.Id);
                    EquipWeapon(weapon);
                }
            }
        }

        public void UpdateData(PlayerData data)
        {
            data.HeroEquipment.WeaponStringId = _weaponId.Name;
            data.HeroEquipment.WeaponIntId = _weaponId.Id;
        }
    }
}