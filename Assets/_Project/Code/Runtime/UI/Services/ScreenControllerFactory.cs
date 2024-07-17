using System;
using System.Collections.Generic;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.EventSystem;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.Logic.WaveLogic;
using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Modules.LocalizationProvider;
using Code.Runtime.Modules.TutorialService;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Controller;

namespace Code.Runtime.UI.Services
{
    public class ScreenControllerFactory : IScreenControllerFactory
    {
        private readonly Dictionary<Type, Func<IScreenController>> _screenControllers = new();

        public ScreenControllerFactory(IGameDataContainer gameData, 
            IEventSystem eventSystem,
            IHeroFactory heroFactory, ISaveLoadService saveLoad,
            AudioService audioService, SceneLoader sceneLoader, LevelLoader levelLoader,
            TutorialService tutorialService, AdvertisementService adService, LocalizationService localService)
        {
            _screenControllers.Add(typeof(MainMenuController),
                () => gameData.PlayerData.TutorialData.IsTutorialCompleted
                    ? new MainMenuController(gameData, audioService, levelLoader, localService)
                    : new TutorialMainMenuController(gameData, audioService,  levelLoader, localService, tutorialService)); 
            
            _screenControllers.Add(typeof(WeaponScreenController),
                () => new WeaponScreenController());
            
            _screenControllers.Add(typeof(AbilityScreenController), () => 
                gameData.PlayerData.TutorialData.IsTutorialCompleted 
                  ?  new AbilityScreenController(saveLoad) 
                  : new TutorialAbilityScreenController(saveLoad, tutorialService));
            
            _screenControllers.Add(typeof(HudController), () => 
                new HudController(gameData, heroFactory, levelLoader, eventSystem, adService));
            
            _screenControllers.Add(typeof(MessageWindowController), () => new MessageWindowController());
            
            _screenControllers.Add(typeof(ArenaSelectionScreenController), 
                () => gameData.PlayerData.TutorialData.IsTutorialCompleted 
                ? new ArenaSelectionScreenController(levelLoader)
                : new TutorialArenaSelectionController(levelLoader, tutorialService));
            
            _screenControllers.Add(typeof(RewardPopupController), 
                () => new RewardPopupController(localService));
            
            _screenControllers.Add(typeof(MissionSummaryWindowController), 
                () => new MissionSummaryWindowController());
        }

        public IScreenController CreateController(Type controller)
        {
            return _screenControllers[controller].Invoke();
        }
    }
}