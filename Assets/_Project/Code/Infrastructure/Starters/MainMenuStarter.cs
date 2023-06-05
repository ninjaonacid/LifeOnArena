using System.Threading;
using Code.Infrastructure.Factory;
using Code.Services;
using Code.StaticData.Levels;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.Infrastructure.Starters
{
    public class MainMenuStarter : IAsyncStartable
    {
        private IHeroFactory _heroFactory;
        private IStaticDataService _staticData;

        public MainMenuStarter(IHeroFactory heroFactory, IStaticDataService staticData)
        {
            _heroFactory = heroFactory;
            _staticData = staticData;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            LevelConfig config = _staticData.ForLevel(SceneManager.GetActiveScene().name);
            
            await _heroFactory.CreateHero(config.HeroInitialPosition);
        }
    }
}