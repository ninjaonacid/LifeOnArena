using System;
using System.Threading;
using System.Threading.Tasks;
using Code.ConfigData.Levels;
using Code.Core.EventSystem;
using Code.Core.SceneManagement;
using Code.CustomEvents;
using Code.Logic.EnemySpawners;
using Code.UI;
using Code.UI.Services;
using Cysharp.Threading.Tasks;

namespace Code.Services
{
    public class LevelController : IDisposable
    {
        private LevelReward _levelReward;

        private int _clearedSpawnersCount;
        private int _enemySpawners;

        private readonly IScreenService _screenService;
        private readonly IEventSystem _eventSystem;
        private readonly SceneLoader _sceneLoader;
        private readonly PlayerControls _controls;

        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        public LevelController(IScreenService screenService,
            IEventSystem eventSystem, PlayerControls controls,
            SceneLoader sceneLoader)
        {
            _screenService = screenService;
            _eventSystem = eventSystem;
            _controls = controls;
            _sceneLoader = sceneLoader;

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

        private async void HeroDead(HeroDeadEvent obj)
        {
            _controls.Player.Disable();

            await UniTask.Delay(TimeSpan.FromSeconds(2),
                DelayType.DeltaTime,
                PlayerLoopTiming.Update, _cancellationToken.Token);
        }


        public void InitCurrentLevel(int enemySpawnersCount)
        {
            _clearedSpawnersCount = 0;
            _enemySpawners = enemySpawnersCount;

        }

        public void NextLevelReward(LevelReward levelReward)
        {
            _levelReward = levelReward;
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
