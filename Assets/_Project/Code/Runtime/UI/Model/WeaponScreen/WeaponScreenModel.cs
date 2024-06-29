using System.Collections.Generic;
using Code.Runtime.Core.Config;
using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.UI.Model.WeaponScreen
{
    public class WeaponScreenModel : IScreenModel
    {
        private readonly IGameDataContainer _gameData;
        private readonly ConfigProvider _configProvider;

        private readonly List<WeaponUIModel> _weaponModels = new();
        private WeaponUIModel _equippedWeapon;

        public WeaponScreenModel(IGameDataContainer gameData, ConfigProvider configProvider)
        {
            _gameData = gameData;
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
                    WeaponDescription = weapon.WeaponDescription,
                    WeaponName = weapon.WeaponName
                    
                };
                
                if (weapon.UnlockRequirement != null)
                {
                    if(weapon.UnlockRequirement.CheckRequirement(_gameData.PlayerData))
                        weaponUIModel.isUnlocked = true;
                }
                else
                {
                    weaponUIModel.isUnlocked = true;
                }

                if (_gameData.PlayerData.HeroEquipment.WeaponIntId == weapon.WeaponId.Id)
                {
                    weaponUIModel.isEquipped = true;
                }
                
                _weaponModels.Add(weaponUIModel);
            }
        }

        public List<WeaponUIModel> GetSlots() => _weaponModels;

        public void EquipWeapon(int index)
        {
            if (_equippedWeapon != null)
            {
                _equippedWeapon.isEquipped = false;
            }

            _weaponModels[index].isEquipped = true;
            _equippedWeapon = _weaponModels[index];
            _gameData.PlayerData.HeroEquipment.WeaponIntId = _equippedWeapon.WeaponId;
        }
        
        public bool IsEquipped(int index)
        {
            return _weaponModels[index].isEquipped;
        }

    }
}
