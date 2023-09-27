using System.Threading;
using Code.ConfigData.Levels;
using Code.Core.Factory;
using Code.Services.AudioService;
using Code.Services.ConfigData;
using Code.UI;
using Code.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.Core.EntryPoints
{
    public class MainMenuStarter : IAsyncStartable
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IConfigProvider _config;
        private readonly IScreenService _screenService;
        private readonly AudioService _audioService;
        private readonly PlayerControls _controls;

        public MainMenuStarter(IHeroFactory heroFactory, IUIFactory uiFactory, 
            IScreenService screenService,
            IConfigProvider config, PlayerControls controls,
            AudioService audioService)
        {
            _heroFactory = heroFactory;
            _config = config;
            _screenService = screenService;
            _controls = controls;
            _audioService = audioService;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            LevelConfig config = _config.ForLevel(SceneManager.GetActiveScene().name);

            //GameObject hero = await _heroFactory.CreateHeroUnregistered
              //  (config.HeroInitialPosition, config.HeroInitialRotation);
              
            //_audioService.PlayBackgroundMusic("TalkShowBack", volume: 1, true);

            _screenService.Open(ScreenID.MainMenu);
        }
        
        private void DisableInput()
        {
            _controls.Disable();
        }
    }
}