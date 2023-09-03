using System;
using System.Collections.Generic;
using Code.Infrastructure.SceneManagement;
using Code.Services.PersistentProgress;
using Code.UI.Controller;

namespace Code.UI.Services
{
    public class ScreenControllerFactory : IScreenControllerFactory
    {
        private readonly Dictionary<Type, Func<IScreenController>> _screenControllers = new();
        private readonly IGameDataContainer _gameData;
        
        public ScreenControllerFactory(IGameDataContainer gameData, SceneLoader sceneLoader)
        {
            _gameData = gameData;
            var mainMenuController = new MainMenuController(_gameData);

            _screenControllers.Add(typeof(MainMenuController), () => new MainMenuController(_gameData));
            _screenControllers.Add(typeof(ShopMenuController), () => new ShopMenuController(sceneLoader));
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
