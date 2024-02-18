using System.Threading;
using Code.ConfigData.Levels;
using Code.Core.Factory;
using Code.Services.ConfigData;
using Code.Services.SaveLoad;
using Code.UI;
using Code.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;
using Code.Core.Audio;

namespace Code.Core.EntryPoints
{
    public class MainMenuStarter : IAsyncStartable
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IConfigProvider _config;
        private readonly IScreenService _screenService;
        private readonly Audio.AudioService _audioService;
        private readonly PlayerControls _controls;
        private readonly ISaveLoadService _saveLoad;

        public MainMenuStarter(IHeroFactory heroFactory, IUIFactory uiFactory, 
            IScreenService screenService,
            IConfigProvider config, ISaveLoadService saveLoad, PlayerControls controls,
            Audio.AudioService audioService)
        {
            _heroFactory = heroFactory;
            _config = config;
            _screenService = screenService;
            _controls = controls;
            _audioService = audioService;
            _saveLoad = saveLoad;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _saveLoad.Cleanup();
            
            LevelConfig config = _config.Level(SceneManager.GetActiveScene().name);

            GameObject hero = await _heroFactory.CreateHeroUnregistered(config.HeroInitialPosition, config.HeroInitialRotation);
            
            DisableInput();
              
            _audioService.PlayBackgroundMusic("MainTheme", volume: 1, true);

            _screenService.Open(ScreenID.MainMenu);
        }
        
        private void DisableInput()
        {
            _controls.Disable();
        }
    }
}