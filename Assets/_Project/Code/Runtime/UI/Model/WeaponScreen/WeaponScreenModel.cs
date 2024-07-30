using System.Collections.Generic;
using Code.Runtime.Core.Config;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.Model.WeaponScreen
{
    public class WeaponScreenModel : IScreenModel
    {
        private readonly PlayerData _playerData;
        private readonly ConfigProvider _configProvider;

        private readonly List<WeaponUIModel> _weaponModels = new();
        private WeaponUIModel _equippedWeapon;

        public WeaponScreenModel(PlayerData playerData, ConfigProvider configProvider)
        {
            _playerData = playerData;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            var weapons = _configProvider.GetHeroWeapons();

            foreach (var weapon in weapons)
            {
                var weaponUIModel = new WeaponUIModel()
                {
                    WeaponIcon = weapon.WeaponIcon,
                    WeaponId = weapon.WeaponId.Id,
                    WeaponName = weapon.WeaponName
                    
                };
                
                if (weapon.UnlockRequirement != null)
                {
                    if(weapon.UnlockRequirement.CheckRequirement(_playerData)) 
                        weaponUIModel.isUnlocked = true;
                }
                else
                {
                    weaponUIModel.isUnlocked = true;
                }

                if (weaponUIModel.isUnlocked)
                {
                    weaponUIModel.WeaponDescription = weapon.WeaponDescription;
                }
                else
                {
                    weaponUIModel.WeaponDescription = weapon.UnlockDescription;
                }

                if (_playerData.HeroEquipment.WeaponIntId == weapon.WeaponId.Id)
                {
                    weaponUIModel.isEquipped = true;
                    _equippedWeapon = weaponUIModel;
                }

                _weaponModels.Add(weaponUIModel);
            }
        }

        public List<WeaponUIModel> GetSlots() => _weaponModels;

        public WeaponUIModel GetModel(int id)
        {
            foreach (var model in _weaponModels)
            {
                if (model.WeaponId == id)
                {
                    return model;
                }
            }

            return null;
        }

        public void EquipWeapon(int id)
        {
            if (_equippedWeapon != null)
            {
                _equippedWeapon.isEquipped = false;
            }

            foreach (var weaponModel in _weaponModels)
            {
                if (weaponModel.WeaponId == id)
                {
                    weaponModel.isEquipped = true;
                    _equippedWeapon = weaponModel;
                    _playerData.HeroEquipment.WeaponIntId = weaponModel.WeaponId;
                }
            }
        }
        
        public bool IsEquipped(int weaponId)
        {
            foreach (var weapon in _weaponModels)
            {
                if (weaponId == weapon.WeaponId)
                {
                    return weapon.isEquipped;
                }
            }

            return false;
        }

        public bool IsUnlocked(int weaponId)
        {
            foreach (var weapon in _weaponModels)
            {
                if (weaponId == weapon.WeaponId)
                {
                    return weapon.isUnlocked;
                }
            }

            return false;
        }
    }
}
