using Code.ConfigData;
using Code.Core.Factory;
using Code.Data.PlayerData;
using Code.Logic.Weapon;
using Code.Services.PersistentProgress;
using UnityEngine;
using VContainer;

namespace Code.Entity.Hero
{
    public class HeroWeapon : EntityWeapon, ISave
    {
        private MeleeWeapon _currentWeapon;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroStateMachineHandler _stateMachine;
        private IItemFactory _itemFactory;

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
            
            _weaponSlot.WeaponData = weaponData;
            _weaponSlot.WeaponId = weaponData.WeaponId;
            _heroAnimator.OverrideController(weaponData.OverrideController);
            _stateMachine.ChangeConfig(weaponData.FsmConfig);

            _currentWeapon = Instantiate(weaponData.WeaponPrefab, _weaponPosition, false).GetComponent<MeleeWeapon>();
            _currentWeapon.transform.localPosition = Vector3.zero;

            _currentWeapon.transform.localRotation = Quaternion.Euler(
                weaponData.Rotation.x,
                weaponData.Rotation.y, 
                weaponData.Rotation.z);
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
