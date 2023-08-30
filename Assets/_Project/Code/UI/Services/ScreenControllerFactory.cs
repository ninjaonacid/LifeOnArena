using System;
using System.Collections.Generic;
using Code.Services.PersistentProgress;
using Code.UI.Controller;
using Code.UI.Model;
using Code.UI.View;

namespace Code.UI.Services
{
    public class ScreenControllerFactory : IScreenControllerFactory
    {
        private readonly Dictionary<Type, Func<IScreenController<IScreenModel, BaseView>>> _screenControllers = new();
        private readonly IGameDataContainer _gameData;
        
        public ScreenControllerFactory(IGameDataContainer gameData)
        {
            _gameData = gameData;
            
            _screenControllers.Add(typeof(MainMenuController), () => new MainMenuController(_gameData));
        }
            
        
        public TController CreateController<TController>()
        {
            throw new System.NotImplementedException();
        }

        public IScreenController<IScreenModel, BaseView> CreateController()
        {
            throw new System.NotImplementedException();
        }

        public TController CreateController<TModel, TView, TController>()
        {
            throw new NotImplementedException();
        }
    }
}
