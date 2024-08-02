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
            HeroFactory heroFactory, SaveLoadService saveLoad,
            AudioService audioService, PlayerControls playerControls,
            LevelCollectableTracker collectableTracker, LevelLoader levelLoader,
            TutorialService tutorialService, PauseService pauseService,
            AdvertisementService adService, LocalizationService localService)
        {
            _screenControllers.Add(typeof(MainMenuController),
                () => gameData.PlayerData.TutorialData.IsTutorialCompleted
                    ? new MainMenuController(gameData, adService, audioService, saveLoad, pauseService)
                    : new TutorialMainMenuController(gameData, adService, audioService, saveLoad, pauseService, tutorialService));

            _screenControllers.Add(typeof(WeaponScreenController),
                () => gameData.PlayerData.TutorialData.IsTutorialCompleted
                    ? new WeaponScreenController(audioService, heroFactory, saveLoad, gameData.PlayerData)
                    : new TutorialWeaponScreenController(audioService, heroFactory, saveLoad, gameData.PlayerData, tutorialService));

            _screenControllers.Add(typeof(AbilityScreenController), () =>
                gameData.PlayerData.TutorialData.IsTutorialCompleted
                    ? new AbilityScreenController(saveLoad, audioService, gameData.PlayerData)
                    : new TutorialAbilityScreenController(saveLoad, audioService, gameData.PlayerData, tutorialService));

            _screenControllers.Add(typeof(HudController), () =>
                new HudController(gameData, heroFactory, levelLoader, eventSystem, adService, pauseService,
                    collectableTracker, localService));

            _screenControllers.Add(typeof(MessageWindowController), () => new MessageWindowController());

            _screenControllers.Add(typeof(ArenaSelectionScreenController),
                () => gameData.PlayerData.TutorialData.IsTutorialCompleted
                    ? new ArenaSelectionScreenController(levelLoader, saveLoad, audioService)
                    : new TutorialArenaSelectionController(levelLoader, saveLoad, audioService, tutorialService));

            _screenControllers.Add(typeof(RewardPopupController),
                () => new RewardPopupController(localService, pauseService));

            _screenControllers.Add(typeof(MissionSummaryWindowController),
                () => new MissionSummaryWindowController(levelLoader, collectableTracker, playerControls));

            _screenControllers.Add(typeof(MainMenuSettingsPopupController),
                () => new MainMenuSettingsPopupController(localService, audioService, saveLoad));

            _screenControllers.Add(typeof(HudSettingsPopupController),
                () => new HudSettingsPopupController(pauseService, levelLoader, audioService, saveLoad, adService));

            _screenControllers.Add(typeof(HeroDeathPopupController),
                () => new HeroDeathPopupController(levelLoader, adService, pauseService, heroFactory, playerControls,
                    audioService, gameData));

            _screenControllers.Add(typeof(HudControlsController),
                () => new HudControlsController(pauseService));
        }

        public IScreenController CreateController(Type controller)
        {
            return _screenControllers[controller].Invoke();
        }
    }
}