using System.Collections.Generic;
using Code.Runtime.Core.Config;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.Localization;

namespace Code.Runtime.UI.Model
{
    public class WeaponScreenModel : IScreenModel
    {
        private IGameDataContainer _gameData;
        private ConfigProvider _configProvider;

        private List<WeaponUIModel> _weaponModels;

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
                    _weaponIcon = weapon.WeaponIcon,
                    
                };
            }
        }
    }

    public class WeaponUIModel
    {
        public Sprite _weaponIcon;
        public LocalizedString _weaponName;
    }
}
