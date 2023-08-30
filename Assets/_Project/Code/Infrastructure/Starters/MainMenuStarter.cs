using System.Threading;
using Code.Infrastructure.Factory;
using Code.Services;
using Code.StaticData.Levels;
using Code.UI;
using Code.UI.Controller;
using Code.UI.Model;
using Code.UI.Services;
using Code.UI.View;
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
        private readonly IScreenViewService _screenViewService;
        private readonly IUIFactory _uiFactory;
        private readonly PlayerControls _controls;

        public MainMenuStarter(IHeroFactory heroFactory, IUIFactory uiFactory, 
            IScreenViewService screenViewService,
            IConfigDataProvider configData, PlayerControls controls)
        {
            _heroFactory = heroFactory;
            _configData = configData;
            _uiFactory = uiFactory;
            _screenViewService = screenViewService;
            _controls = controls;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            LevelConfig config = _configData.ForLevel(SceneManager.GetActiveScene().name);
            
            _uiFactory.CreateCore();

            GameObject hero = await _heroFactory.CreateHeroUnregistered(config.HeroInitialPosition);

           // ScreenBase mainMenu = _uiFactory.CreateMainMenu(_screenViewService);

           _screenViewService.Show<MainMenuModel, MainMenuView, MainMenuController>(ScreenID.SelectionMenu);

        }
        
        private void DisableInput()
        {
            _controls.Disable();
        }
    }
}