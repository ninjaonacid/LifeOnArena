using System;
using System.Collections.Generic;
using Code.Core.Factory;
using Code.Core.SceneManagement;
using Code.Services.AudioService;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.UI.Controller;

namespace Code.UI.Services
{
    public class ScreenControllerFactory : IScreenControllerFactory
    {
        private readonly Dictionary<Type, Func<IScreenController>> _screenControllers = new();

        public ScreenControllerFactory(IGameDataContainer gameData, IHeroFactory heroFactory, ISaveLoadService saveLoad, AudioService audioService, SceneLoader sceneLoader)
        {
            _screenControllers.Add(typeof(MainMenuController), () => new MainMenuController(gameData, audioService, sceneLoader));
            _screenControllers.Add(typeof(ShopMenuController), () => new ShopMenuController(sceneLoader));
            _screenControllers.Add(typeof(AbilityMenuController), () => new AbilityMenuController(saveLoad));
            _screenControllers.Add(typeof(HudController), () => new HudController(gameData, heroFactory));
        }

        public IScreenController CreateController(Type controller)
        {
            return _screenControllers[controller].Invoke();
        }
        
        
    }
}
