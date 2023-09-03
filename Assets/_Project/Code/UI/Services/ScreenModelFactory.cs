using System;
using System.Collections.Generic;
using Code.Services.PersistentProgress;
using Code.UI.Model;

namespace Code.UI.Services
{
    public class ScreenModelFactory : IScreenModelFactory
    {
        private Dictionary<Type, Func<IScreenModel>> _modelMap = new();

        private IGameDataContainer _gameData;

        public ScreenModelFactory(IGameDataContainer gameData)
        {
            _gameData = gameData;
            
            
            _modelMap.Add(typeof(MainMenuModel), () => new MainMenuModel(_gameData.PlayerData.StatsData));
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
