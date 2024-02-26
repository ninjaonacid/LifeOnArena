using System;
using System.Collections.Generic;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.AbilityMenu;

namespace Code.Runtime.UI.Services
{
    public class ScreenModelFactory : IScreenModelFactory
    {
        private readonly Dictionary<Type, Func<IScreenModel>> _modelMap = new();
        private readonly ISaveLoadService _saveLoad;

        public ScreenModelFactory(IGameDataContainer gameData, IConfigProvider config, ISaveLoadService saveLoad)
        {
            _saveLoad = saveLoad;

            InitializeModelMap(gameData, config);
        }

        private void InitializeModelMap(IGameDataContainer gameData, IConfigProvider config)
        {
            _modelMap.Add(typeof(MainMenuModel), () => new MainMenuModel(gameData));
            _modelMap.Add(typeof(WeaponShopMenuModel), () => new WeaponShopMenuModel());
            _modelMap.Add(typeof(AbilityMenuModel), () => new AbilityMenuModel(gameData, config));
            _modelMap.Add(typeof(HudModel), () => new HudModel());
        }

        public IScreenModel CreateModel(Type model)
        {
            var modelInstance = _modelMap[model].Invoke();
            modelInstance.Initialize();
            return modelInstance;
        }
    }
}
