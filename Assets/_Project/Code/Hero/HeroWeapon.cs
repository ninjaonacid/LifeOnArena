using System;
using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services.PersistentProgress;
using Code.StaticData;
using Code.StaticData.Identifiers;
using UnityEngine;
using VContainer;

namespace Code.Hero
{
    public class HeroWeapon : MonoBehaviour, ISave
    {
        public Transform WeaponPosition;
        public HeroSkills HeroSkills;
        private readonly WeaponSlot _weapon = new();

        private GameObject _currentWeapon;
        private IItemFactory _itemFactory;

        public class WeaponSlot
        {
            public WeaponId WeaponId;
            public WeaponData WeaponData;
        }

        [Inject]
        public void Construct(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        public void EquipWeapon(WeaponData weaponData)
        {
            if (weaponData == null) return;

            if (_currentWeapon != null)
                Destroy(_currentWeapon.gameObject);

            _weapon.WeaponData = weaponData;
            _weapon.WeaponId = weaponData.WeaponId;

            _currentWeapon = Instantiate(weaponData.WeaponPrefab, WeaponPosition, false);
            _currentWeapon.transform.localPosition = Vector3.zero;

            _currentWeapon.transform.localRotation = Quaternion.Euler(
                weaponData.Rotation.x,
                weaponData.Rotation.y, 
                weaponData.Rotation.z);

            //HeroSkills.ChangeSkill(weaponData.abilityData);
        }

        public WeaponData GetEquippedWeapon()
        {
            if (_weapon != null)
            {
                return _weapon.WeaponData;
            }

            return null;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _weapon.WeaponId = progress.HeroEquipment.HeroWeapon;
            if (_weapon != null)
            {
                _weapon.WeaponData = _itemFactory.LoadWeapon(_weapon.WeaponId);
                EquipWeapon(_weapon.WeaponData);
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroEquipment.HeroWeapon = _weapon.WeaponId;
        }
    }
}
