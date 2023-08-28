using System;
using System.Threading;
using Code.CustomEvents;
using Code.Infrastructure.EventSystem;
using Code.Logic.EnemySpawners;
using Code.StaticData.Levels;
using Code.UI;
using Code.UI.Services;
using Cysharp.Threading.Tasks;

namespace Code.Services
{
    public class LevelEventHandler : ILevelEventHandler
    {

        public event Action MonsterSpawnersCleared;
        public event Action PlayerDead;

        private LevelReward _levelReward;

        private int _clearedSpawnersCount;
        private int _enemySpawners;

        private readonly IScreenViewService _screenViewService;
        private readonly IEventSystem _eventSystem;

        private CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        public LevelEventHandler(IScreenViewService screenViewService, IEventSystem eventSystem)
        {
            _screenViewService = screenViewService;
            _eventSystem = eventSystem;
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

        public void HeroDeath()
        {
            PlayerDead?.Invoke();
        }

        public async void MonsterSpawnerSlain(EnemySpawner spawner)
        {
            _clearedSpawnersCount++;

            if (_clearedSpawnersCount == _enemySpawners)
            {
                MonsterSpawnersCleared?.Invoke();

                _eventSystem.FireEvent(new OpenDoorEvent("door opened"));
                await ShowUpgradeWindow();
            }
        }

        public async UniTask ShowUpgradeWindow()
        {
            
            await UniTask.Delay(TimeSpan.FromSeconds(2.0), 
                DelayType.DeltaTime, 
                PlayerLoopTiming.Update,
                _cancellationToken.Token);

            _screenViewService.Open(ScreenID.UpgradeMenu);
        }
        public LevelReward GetLevelReward() => _levelReward;
        
    }
}
