using System;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Logic.Weapon;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    public class HeroWeapon : EntityWeapon, ISave
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        public event Action<Weapon> OnWeaponChange;

        public override void EquipWeapon(WeaponData weaponData)
        {
            base.EquipWeapon(weaponData);

            _heroAnimator.OverrideController(weaponData.OverrideController);

            OnWeaponChange?.Invoke(_weaponSlot.EquippedWeapon);
        }

        public void LoadData(PlayerData data)
        {
            var weaponId = data.HeroEquipment.WeaponIntId;

            if (weaponId != 0)
            {
                WeaponData weaponData = _itemFactory.LoadWeapon(weaponId);
                EquipWeapon(weaponData);
            }
            else
            {
                if (_weaponData)
                {
                    EquipWeapon(_weaponData);
                }
            }
        }

        public void UpdateData(PlayerData data)
        {
            data.HeroEquipment.WeaponStringId = _weaponData.WeaponId.Name;
            data.HeroEquipment.WeaponIntId = _weaponData.WeaponId.Id;
        }
    }
}