using System.Threading;
using Code.Infrastructure.Factory;
using Code.Infrastructure.InputSystem;
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
        private readonly IStaticDataService _staticData;
        private readonly IInputSystem _input;
        private readonly IWindowService _windowService;
        private readonly IUIFactory _uiFactory;

        public MainMenuStarter(IHeroFactory heroFactory, IUIFactory uiFactory, 
            IWindowService windowService,
            IStaticDataService staticData, IInputSystem input)
        {
            _heroFactory = heroFactory;
            _staticData = staticData;
            _input = input;
            _uiFactory = uiFactory;
            _windowService = windowService;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            LevelConfig config = _staticData.ForLevel(SceneManager.GetActiveScene().name);
            
            _uiFactory.CreateCore();

            GameObject hero = await _heroFactory.CreateHero(config.HeroInitialPosition);

            ScreenBase mainMenu = _uiFactory.CreateMainMenu(_windowService);
            
        }
        
        private void DisableInput()
        {
            _input.Disable();
        }
    }
}