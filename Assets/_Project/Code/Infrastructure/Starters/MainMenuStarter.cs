using System.Threading;
using Code.Infrastructure.Factory;
using Code.Infrastructure.InputSystem;
using Code.Services;
using Code.StaticData.Levels;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.Infrastructure.Starters
{
    public class MainMenuStarter : IAsyncStartable
    {
        private readonly IHeroFactory _heroFactory;
        private readonly IStaticDataService _staticData;
        private readonly IInputSystem _input;
        
        public MainMenuStarter(IHeroFactory heroFactory, IStaticDataService staticData, IInputSystem input)
        {
            _heroFactory = heroFactory;
            _staticData = staticData;
            _input = input;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            LevelConfig config = _staticData.ForLevel(SceneManager.GetActiveScene().name);
            
            await _heroFactory.CreateHero(config.HeroInitialPosition);
            
            _input.Disable();
            
        }
    }
}