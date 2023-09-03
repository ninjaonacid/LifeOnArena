using System;
using System.Collections.Generic;
using Code.Services.PersistentProgress;
using Code.UI.Controller;

namespace Code.UI.Services
{
    public class ScreenControllerFactory : IScreenControllerFactory
    {
        private readonly Dictionary<Type, Func<IScreenController>> _screenControllers = new();
        private readonly IGameDataContainer _gameData;
        
        public ScreenControllerFactory(IGameDataContainer gameData)
        {
            _gameData = gameData;
            var mainMenuController = new MainMenuController(_gameData);

            _screenControllers.Add(typeof(MainMenuController), CreateMainMenuController);
        }
        
        private IScreenController CreateMainMenuController()
        {
            return new MainMenuController(_gameData);
        }

        public IScreenController CreateController<TModel, TView, TController>()
        {
            throw new NotImplementedException();
        }

        public IScreenController CreateController(Type controller)
        {
            return _screenControllers[controller].Invoke();
        }
        
        
    }
}
