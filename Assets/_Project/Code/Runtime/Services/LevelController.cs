using System;
using System.Threading;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.EventSystem;
using Code.Runtime.CustomEvents;
using Code.Runtime.Logic.WaveLogic;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI;
using Code.Runtime.UI.Model.DTO;
using Code.Runtime.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Code.Runtime.Services
{
    public class LevelController : IInitializable, ITickable, IDisposable
    {
        private LevelReward _levelReward;
        
        private int _enemySpawners;
        private int _timerToEndOfLevel = 5;

        private EnemySpawnerController _spawnerController;
        private readonly ScreenService _screenService;
        private readonly IEventSystem _eventSystem;
        private readonly LevelLoader _levelLoader;
        private readonly PlayerControls _controls;
        private readonly IGameDataContainer _gameData;

        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        public LevelController(EnemySpawnerController spawnerController, ScreenService screenService,
            IEventSystem eventSystem, PlayerControls controls,
            LevelLoader levelLoader, IGameDataContainer gameData)
        {
            _spawnerController = spawnerController;
            _screenService = screenService;
            _eventSystem = eventSystem;
            _controls = controls;
            _levelLoader = levelLoader;
            _gameData = gameData;
        }

        public void Initialize()
        {
            _spawnerController.WaveCleared += WaveCleared;
            _spawnerController.CommonEnemiesCleared += CommonEnemiesCleared;
            //_spawnerController.BossKilled += LevelEnd;
            _spawnerController.BossSpawned += OnBossSpawn;
            _controls.LevelControls.Enable();
            
        }

        private void OnBossSpawn(GameObject arg1, MobIdentifier arg2)
        {
            _eventSystem.FireEvent(new BossSpawnEvent(arg1, arg2));
        }

        private void LevelCompleted()
        {
            
        }
        
        private async UniTask LevelEnd()
        {
            var currentLevel = _levelLoader.GetCurrentLevelConfig();
            _gameData.PlayerData.WorldData.LocationProgressData.CompletedLocations.Add(currentLevel.LevelId.Id);
            _screenService.Open(ScreenID.MessageWindow, new TimerMessageDto("Return to camp : ", _timerToEndOfLevel ));
            await UniTask.Delay(TimeSpan.FromSeconds(_timerToEndOfLevel));
            _levelLoader.LoadLevel("MainMenu");
        }

        private void CommonEnemiesCleared(int secondsToBoss)
        {
            var levelConfig = _levelLoader.GetCurrentLevelConfig();
            
            if (levelConfig.IsBossLevel)
            {
                _screenService.Open(ScreenID.MessageWindow, new TimerMessageDto("Boss reveal in : ", _spawnerController.TimeToNextWave));
            }
            else
            {
                LevelEnd().Forget();
            }
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

        private void WaveCleared(int secondsToNextWave)
        {
            _screenService.Open(ScreenID.MessageWindow, new TimerMessageDto("Next wave", _spawnerController.TimeToNextWave));
        }

        private async void HeroDead(HeroDeadEvent obj)
        {
            _controls.Player.Disable();
            
            _screenService.Open(ScreenID.MessageWindow, new MessageDto("You died"));
            
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

        public void Dispose()
        {
            _eventSystem.Unsubscribe<HeroDeadEvent>(HeroDead);
            _eventSystem.Unsubscribe<SpawnersClearEvent>(SpawnersClear);
            
            
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();

        }

        public void Tick()
        {
            if (_controls.LevelControls.Button.triggered)
            {
                _levelLoader.LoadLevel("FantasyArena_1");
            }if (_controls.LevelControls.Button1.triggered)
            {
                _levelLoader.LoadLevel("FantasyArena_2");
            }if (_controls.LevelControls.Button2.triggered)
            {
                _levelLoader.LoadLevel("FantasyArena_3");
            }
        }
    }
}
