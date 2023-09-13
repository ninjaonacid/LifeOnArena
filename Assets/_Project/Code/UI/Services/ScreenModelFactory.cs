using System;
using System.Collections.Generic;
using Code.Services.PersistentProgress;
using Code.UI.Model;

namespace Code.UI.Services
{
    public class ScreenModelFactory : IScreenModelFactory
    {
        private Dictionary<Type, Func<IScreenModel>> _modelMap = new();

        public ScreenModelFactory(IGameDataContainer gameData)
        {
            _modelMap.Add(typeof(MainMenuModel), () => new MainMenuModel(gameData.PlayerData.StatsData));
            _modelMap.Add(typeof(ShopMenuModel), () => new ShopMenuModel());
            _modelMap.Add(typeof(AbilityMenuModel), () => new AbilityMenuModel());
        }
        
        public TModel CreateModel<TModel>() where TModel : IScreenModel
        {
            if (_modelMap.TryGetValue(typeof(TModel), out var model))
            {
                var modelInstance = (TModel)model.Invoke();
                modelInstance.Initialize();
                return modelInstance;
            }

            return default;
        }

        public IScreenModel CreateModel(Type model)
        {
            var modelInstance = _modelMap[model].Invoke();
            modelInstance.Initialize();
            return modelInstance;
        }
    }
}
