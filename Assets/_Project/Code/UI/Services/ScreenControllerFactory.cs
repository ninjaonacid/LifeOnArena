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

        public ScreenControllerFactory(IGameDataContainer gameData, SceneLoader sceneLoader)
        {
            _screenControllers.Add(typeof(MainMenuController), () => new MainMenuController(gameData));
            _screenControllers.Add(typeof(ShopMenuController), () => new ShopMenuController(sceneLoader));
        }

        public IScreenController CreateController(Type controller)
        {
            return _screenControllers[controller].Invoke();
        }
        
        
    }
}
