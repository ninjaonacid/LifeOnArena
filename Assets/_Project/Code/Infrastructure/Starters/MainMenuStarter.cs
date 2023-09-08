using System.Threading;
using Code.Infrastructure.Factory;
using Code.Services;
using Code.StaticData.Levels;
using Code.UI;
using Code.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.Infrastructure.Starters
{
    public class MainMenuStarter : IAsyncStartable
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IConfigDataProvider _configData;
        private readonly IScreenService _screenService;
        private readonly PlayerControls _controls;

        public MainMenuStarter(IHeroFactory heroFactory, IUIFactory uiFactory, 
            IScreenService screenService,
            IConfigDataProvider configData, PlayerControls controls)
        {
            _heroFactory = heroFactory;
            _configData = configData;
            _screenService = screenService;
            _controls = controls;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            LevelConfig config = _configData.ForLevel(SceneManager.GetActiveScene().name);

            //GameObject hero = await _heroFactory.CreateHeroUnregistered
              //  (config.HeroInitialPosition, config.HeroInitialRotation);

            _screenService.Open(ScreenID.MainMenu);
        }
        
        private void DisableInput()
        {
            _controls.Disable();
        }
    }
}