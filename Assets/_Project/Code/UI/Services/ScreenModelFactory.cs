using System;
using System.Collections.Generic;
using Code.Services.PersistentProgress;
using Code.UI.Model;

namespace Code.UI.Services
{
    public class ScreenModelFactory : IScreenModelFactory
    {
        private Dictionary<Type, IScreenModel> _modelMap = new();

        private GameDataContainer _gameDataContainer;

        public ScreenModelFactory()
        {
            _modelMap.Add(typeof(MainMenuModel), new MainMenuModel());
        }

        public IScreenModel CreateModel<TModel>(ScreenID screenId)
        {
            return _modelMap[typeof(TModel)];
        }

        public IScreenModel CreateModel(Type model)
        {
            return null;
        }
    }
}
