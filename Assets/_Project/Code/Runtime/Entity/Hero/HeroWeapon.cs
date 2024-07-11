using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    public class HeroWeapon : EntityWeapon, ISave
    {
        [SerializeField] private HeroAnimator _heroAnimator;
        

        protected override void Start()
        {
        }

        public override void EquipWeapon(WeaponData weaponData)
        {
            base.EquipWeapon(weaponData);
            if(weaponData.OverrideController != null)
                _heroAnimator.OverrideController(weaponData.OverrideController);
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
            data.HeroEquipment.WeaponIntId = _weaponData.WeaponId.Id;
        }
    }
}