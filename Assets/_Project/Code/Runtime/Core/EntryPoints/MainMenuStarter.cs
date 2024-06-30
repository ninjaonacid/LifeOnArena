using System.Threading;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.Factory;
using Code.Runtime.Services.LevelLoader;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI;
using Code.Runtime.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.Runtime.Core.EntryPoints
{
    public class MainMenuStarter : IAsyncStartable
    {
        private readonly IHeroFactory _heroFactory;
        private readonly LevelLoader _levelLoader;
        private readonly ScreenService _screenService;
        private readonly AudioService _audioService;
        private readonly PlayerControls _controls;
        private readonly ISaveLoadService _saveLoad;

        public MainMenuStarter(IHeroFactory heroFactory, IUIFactory uiFactory,
            ScreenService screenService,
            LevelLoader levelLoader, ISaveLoadService saveLoad, PlayerControls controls,
            AudioService audioService)
        {
            _heroFactory = heroFactory;
            _levelLoader = levelLoader;
            _screenService = screenService;
            _controls = controls;
            _audioService = audioService;
            _saveLoad = saveLoad;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _saveLoad.Cleanup();

            LevelConfig config = _levelLoader.GetCurerntLevelConfig();

            GameObject hero = await _heroFactory.CreateHero(config.HeroInitialPosition, config.HeroInitialRotation);

            var transformRotation = hero.transform.rotation;
            transformRotation.eulerAngles =  new Vector3(0, 180, 0);
            hero.transform.rotation = transformRotation;

            DisableInput();
            
            _saveLoad.LoadData();

            //_audioService.PlayBackgroundMusic("MainTheme", volume: 1, true);

            _screenService.Open(ScreenID.MainMenu);
        }

        private void DisableInput()
        {
            _controls.Disable();
        }
    }
}