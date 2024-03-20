using System;
using System.Threading;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.EventSystem;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.CustomEvents;
using Code.Runtime.Logic.WaveLogic;
using Code.Runtime.UI;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Code.Runtime.Services
{
    public class LevelController : IInitializable, IDisposable
    {
        private LevelReward _levelReward;
        
        private int _enemySpawners;

        private EnemySpawnerController _spawnerController;
        private readonly ScreenService _screenService;
        private readonly IEventSystem _eventSystem;
        private readonly SceneLoader _sceneLoader;
        private readonly PlayerControls _controls;

        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        public LevelController(EnemySpawnerController spawnerController, ScreenService screenService,
            IEventSystem eventSystem, PlayerControls controls,
            SceneLoader sceneLoader)
        {
            _spawnerController = spawnerController;
            _screenService = screenService;
            _eventSystem = eventSystem;
            _controls = controls;
            _sceneLoader = sceneLoader;
            
        }

        public void Initialize()
        {
           _spawnerController.WaveCleared += 
        }

        public void Subscribe()
        {
            _eventSystem.Subscribe<HeroDeadEvent>(HeroDead);
            _eventSystem.Subscribe<SpawnersClearEvent>(SpawnersClear);
        }

        private async void SpawnersClear(SpawnersClearEvent obj)
        {
            await ShowUpgradeWindow();
            
            _eventSystem.FireEvent(new OpenDoorEvent("open door"));
        }

        private async void WaveCleared(int secondsToNextWave)
        {
            _screenService.OpenWithParameters(ScreenID.MessageWindow, new MessageWindowDto("next"));
        }

        private async void HeroDead(HeroDeadEvent obj)
        {
            _controls.Player.Disable();
            
            _screenService.OpenWithParameters(ScreenID.MessageWindow, new MessageWindowDto("You died"));

            await UniTask.Delay(TimeSpan.FromSeconds(2),
                DelayType.DeltaTime,
                PlayerLoopTiming.Update, _cancellationToken.Token);
            
        }

        private async UniTask ShowUpgradeWindow()
        {
            
            await UniTask.Delay(TimeSpan.FromSeconds(2.0), 
                DelayType.DeltaTime, 
                PlayerLoopTiming.Update,
                _cancellationToken.Token);

            _screenService.Open(ScreenID.UpgradeMenu);
        }

        public LevelReward GetLevelReward() => _levelReward;

        public void Dispose()
        {
            _cancellationToken?.Dispose();
            
            _eventSystem.Unsubscribe<HeroDeadEvent>(HeroDead);
        }
    }
}
