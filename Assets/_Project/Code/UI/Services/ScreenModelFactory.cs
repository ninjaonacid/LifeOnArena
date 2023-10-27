using System;
using System.Collections.Generic;
using Code.Services.ConfigData;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.UI.Model;
using Code.UI.Model.AbilityMenu;

namespace Code.UI.Services
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
            _modelMap.Add(typeof(ShopMenuModel), () => new ShopMenuModel());
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
