using System;
using System.Collections.Generic;
using Code.Core.Factory;
using Code.Services.PersistentProgress;
using Code.UI.Model;

namespace Code.UI.Services
{
    public class ScreenModelFactory : IScreenModelFactory
    {
        private Dictionary<Type, Func<IScreenModel>> _modelMap = new();

        public ScreenModelFactory(IGameDataContainer gameData, IHeroFactory heroFactory)
        {
            _modelMap.Add(typeof(MainMenuModel), () => new MainMenuModel(gameData));
            _modelMap.Add(typeof(ShopMenuModel), () => new ShopMenuModel());
            _modelMap.Add(typeof(AbilityMenuModel), () => new AbilityMenuModel());
            _modelMap.Add(typeof(HudModel), () => new HudModel(heroFactory));
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
