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
        public event Action<MeleeWeapon, WeaponId> OnWeaponChange;
        private MeleeWeapon CurrentWeapon { get; set; }
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
                WeaponData weapon = _itemFactory.LoadWeapon(weaponId);
                EquipWeapon(weapon);
            }
            else
            {
                if (_weaponSlot.WeaponData)
                {
                    WeaponData weapon = _itemFactory.LoadWeapon(_weaponSlot.WeaponData.WeaponId.Id);
                    EquipWeapon(weapon);
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