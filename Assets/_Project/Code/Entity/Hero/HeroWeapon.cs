using Code.Data;
using Code.Infrastructure.Factory;
using Code.Services.PersistentProgress;
using Code.StaticData;
using Code.StaticData.Identifiers;
using UnityEngine;
using VContainer;

namespace Code.Entity.Hero
{
    public class HeroWeapon : EntityWeapon, ISave
    {
        private readonly WeaponSlot _weapon = new();

        private GameObject _currentWeapon;
        private IItemFactory _itemFactory;
        
        private class WeaponSlot
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

            _currentWeapon = Instantiate(weaponData.WeaponPrefab, _weaponPosition, false);
            _currentWeapon.transform.localPosition = Vector3.zero;

            _currentWeapon.transform.localRotation = Quaternion.Euler(
                weaponData.Rotation.x,
                weaponData.Rotation.y, 
                weaponData.Rotation.z);
        }
        

        public void LoadData(PlayerData data)
        {
            _weapon.WeaponId = data.HeroEquipment.HeroWeapon;
            if (_weapon != null)
            {
                _weapon.WeaponData = _itemFactory.LoadWeapon(_weapon.WeaponId);
                EquipWeapon(_weapon.WeaponData);
            }
        }

        public void UpdateData(PlayerData data)
        {
            data.HeroEquipment.HeroWeapon = _weapon.WeaponId;
        }
    }
}
