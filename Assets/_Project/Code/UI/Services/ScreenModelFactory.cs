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
            
            
        }

        public IScreenModel CreateModel<TModel>(ScreenID screenId)
        {
            throw new NotImplementedException();
        }

        public IScreenModel CreateModel(Type model)
        {
            
        }
    }
}
