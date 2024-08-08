
using System.Threading;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.LevelLoaderService;
using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI;
using Code.Runtime.UI.Services;
using Cysharp.Threading.Tasks;
using GamePush;
using UnityEngine;
using VContainer.Unity;

namespace Code.Runtime.Core.EntryPoints
{
    public class MainMenuStarter : IAsyncStartable
    {
        private readonly HeroFactory _heroFactory;
        private readonly LevelLoader _levelLoader;
        private readonly ScreenService _screenService;
        private readonly AudioService _audioService;
        private readonly PlayerControls _controls;
        private readonly SaveLoadService _saveLoad;
        private readonly AdvertisementService _adService;

        public MainMenuStarter(HeroFactory heroFactory, UIFactory uiFactory,
            ScreenService screenService,
            LevelLoader levelLoader, SaveLoadService saveLoad, 
            PlayerControls controls,
            AudioService audioService, AdvertisementService adService)
        {
            _heroFactory = heroFactory;
            _levelLoader = levelLoader;
            _screenService = screenService;
            _controls = controls;
            _audioService = audioService;
            _saveLoad = saveLoad;
            _adService = adService;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _saveLoad.Cleanup();

            LevelConfig config = _levelLoader.GetCurrentLevelConfig();

            GameObject hero = await _heroFactory.CreateHero(config.HeroInitialPosition, config.HeroInitialRotation);

            DisableInput();
            
            _saveLoad.LoadData();

            _audioService.PlayMusic("MainTheme", volume: 0.7f, true);

            _screenService.Open(ScreenID.MainMenu);
            
            GP_Game.GameReady();
            GP_Game.GameplayStop();
        }

        private void DisableInput()
        {
            _controls.Disable();
        }
    }
}