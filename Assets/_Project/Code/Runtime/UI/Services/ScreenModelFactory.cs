using System;
using System.Collections.Generic;
using Code.Runtime.Core.Config;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.AbilityMenu;
using Code.Runtime.UI.Model.ArenaSelectionScreenModel;
using Code.Runtime.UI.Model.DTO;
using Code.Runtime.UI.Model.MissionSummaryWindowModel;
using Code.Runtime.UI.Model.WeaponScreen;

namespace Code.Runtime.UI.Services
{
    public class ScreenModelFactory 
    {
        private readonly Dictionary<Type, Func<IScreenModelDto, IScreenModel>> _modelMap = new();
        private readonly ISaveLoadService _saveLoad;

        public ScreenModelFactory(IGameDataContainer gameData, ConfigProvider config, ISaveLoadService saveLoad)
        {
            _saveLoad = saveLoad;

            InitializeModelMap(gameData, config);
        }

        private void InitializeModelMap(IGameDataContainer gameData, ConfigProvider config)
        {
            _modelMap.Add(typeof(MainMenuModel), (dto) => new MainMenuModel(gameData));
            _modelMap.Add(typeof(WeaponScreenModel), (dto) => new WeaponScreenModel(gameData, config));
            _modelMap.Add(typeof(AbilityScreenModel), (dto) => new AbilityScreenModel(gameData, config));
            _modelMap.Add(typeof(HudModel), (dto) => new HudModel());
            _modelMap.Add(typeof(MessageWindowCompositeModel), (dto) => new MessageWindowCompositeModel(dto));
            _modelMap.Add(typeof(ArenaSelectionScreenModel), (dto) => new ArenaSelectionScreenModel(config, gameData));
            _modelMap.Add(typeof(RewardPopupModel), (dto) => new RewardPopupModel(dto));
            _modelMap.Add(typeof(MissionSummaryWindowModel), (dto) => new MissionSummaryWindowModel(dto));
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
