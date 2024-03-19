using System;
using System.Collections.Generic;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.AbilityMenu;
using UnityEditor.VersionControl;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Services
{
    public class ScreenModelFactory 
    {
        private readonly Dictionary<Type, Func<IScreenModelDto, IScreenModel>> _modelMap = new();
        private readonly ISaveLoadService _saveLoad;

        public ScreenModelFactory(IGameDataContainer gameData, IConfigProvider config, ISaveLoadService saveLoad)
        {
            _saveLoad = saveLoad;

            InitializeModelMap(gameData, config);
        }

        private void InitializeModelMap(IGameDataContainer gameData, IConfigProvider config)
        {
            _modelMap.Add(typeof(MainMenuModel), (dto) => new MainMenuModel(gameData));
            _modelMap.Add(typeof(WeaponShopMenuModel), (dto) => new WeaponShopMenuModel());
            _modelMap.Add(typeof(AbilityMenuModel), (dto) => new AbilityMenuModel(gameData, config));
            _modelMap.Add(typeof(HudModel), (dto) => new HudModel());
            _modelMap.Add(typeof(MessageWindowModel), (dto) => new MessageWindowModel(dto));
        }

        public IScreenModel CreateModel(Type model)
        {
            if (!_modelMap.ContainsKey(model))
            {
                throw new ArgumentException($"Model of type {model.Name} is not registered.");
            }
            
            var modelInstance = _modelMap[model].Invoke(default);
            modelInstance.Initialize();
            return modelInstance;
        }
        
        public IScreenModel CreateModel(Type model, IScreenModelDto dto)
        {
            if (!_modelMap.ContainsKey(model))
            {
                throw new ArgumentException($"Model of type {model.Name} is not registered.");
            }
            
            var modelInstance = _modelMap[model].Invoke(dto);
            modelInstance.Initialize();
            return modelInstance;
        }
        
    }
}
