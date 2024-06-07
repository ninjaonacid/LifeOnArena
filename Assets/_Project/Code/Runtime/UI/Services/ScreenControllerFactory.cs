using System;
using System.Collections.Generic;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.Modules.TutorialService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Controller;

namespace Code.Runtime.UI.Services
{
    public class ScreenControllerFactory : IScreenControllerFactory
    {
        private readonly Dictionary<Type, Func<IScreenController>> _screenControllers = new();

        public ScreenControllerFactory(IGameDataContainer gameData, IHeroFactory heroFactory, ISaveLoadService saveLoad,
            AudioService audioService, SceneLoader sceneLoader, TutorialService tutorialService)
        {
            _screenControllers.Add(typeof(MainMenuController),
                () => gameData.PlayerData.TutorialData.IsTutorialCompleted
                    ? new MainMenuController(gameData, audioService, sceneLoader)
                    : new TutorialMainMenuController(gameData, audioService, sceneLoader, tutorialService)); 
            
            _screenControllers.Add(typeof(WeaponShopScreenController),
                () => new WeaponShopScreenController(sceneLoader));
            
            _screenControllers.Add(typeof(AbilityScreenController), () => 
                gameData.PlayerData.TutorialData.IsTutorialCompleted 
                  ?  new AbilityScreenController(saveLoad) 
                  : new TutorialAbilityScreenController(saveLoad, tutorialService));
            
            _screenControllers.Add(typeof(HudController), () => 
                new HudController(gameData, heroFactory, sceneLoader));
            
            _screenControllers.Add(typeof(MessageWindowController), () => new MessageWindowController());
            
            _screenControllers.Add(typeof(ArenaSelectionScreenController), () => new ArenaSelectionScreenController());
        }

        public IScreenController CreateController(Type controller)
        {
            return _screenControllers[controller].Invoke();
        }
    }
}