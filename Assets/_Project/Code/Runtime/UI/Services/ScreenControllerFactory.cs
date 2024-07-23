using System;
using System.Collections.Generic;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.EventSystem;
using Code.Runtime.Core.Factory;
using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Modules.LocalizationProvider;
using Code.Runtime.Modules.TutorialService;
using Code.Runtime.Services;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PauseService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Controller;

namespace Code.Runtime.UI.Services
{
    public class ScreenControllerFactory
    {
        private readonly Dictionary<Type, Func<IScreenController>> _screenControllers = new();

        public ScreenControllerFactory(IGameDataContainer gameData,
            IEventSystem eventSystem,
            HeroFactory heroFactory, ISaveLoadService saveLoad,
            AudioService audioService, LevelCollectableTracker collectableTracker, LevelLoader levelLoader,
            TutorialService tutorialService, PauseService pauseService,
            AdvertisementService adService, LocalizationService localService)
        {
            _screenControllers.Add(typeof(MainMenuController),
                () => gameData.PlayerData.TutorialData.IsTutorialCompleted
                    ? new MainMenuController(gameData, audioService, levelLoader)
                    : new TutorialMainMenuController(gameData, audioService, levelLoader, localService,
                        tutorialService));

            _screenControllers.Add(typeof(WeaponScreenController),
                () => new WeaponScreenController());

            _screenControllers.Add(typeof(AbilityScreenController), () =>
                gameData.PlayerData.TutorialData.IsTutorialCompleted
                    ? new AbilityScreenController(saveLoad)
                    : new TutorialAbilityScreenController(saveLoad, tutorialService));

            _screenControllers.Add(typeof(HudController), () =>
                new HudController(gameData, heroFactory, levelLoader, eventSystem, adService, pauseService, collectableTracker));

            _screenControllers.Add(typeof(MessageWindowController), () => new MessageWindowController());

            _screenControllers.Add(typeof(ArenaSelectionScreenController),
                () => gameData.PlayerData.TutorialData.IsTutorialCompleted
                    ? new ArenaSelectionScreenController(levelLoader)
                    : new TutorialArenaSelectionController(levelLoader, tutorialService));

            _screenControllers.Add(typeof(RewardPopupController),
                () => new RewardPopupController(localService, pauseService));

            _screenControllers.Add(typeof(MissionSummaryWindowController),
                () => new MissionSummaryWindowController(levelLoader, collectableTracker));
            
            _screenControllers.Add(typeof(MainMenuSettingsPopupController), 
            () => new MainMenuSettingsPopupController(localService, audioService));
            
            _screenControllers.Add(typeof(HudSettingsPopupController), 
                () => new HudSettingsPopupController(pauseService, levelLoader, audioService));
            
            _screenControllers.Add(typeof(HeroDeathPopupController), 
                () => new HeroDeathPopupController(levelLoader, adService, pauseService));
        }

        public IScreenController CreateController(Type controller)
        {
            return _screenControllers[controller].Invoke();
        }
    }
}