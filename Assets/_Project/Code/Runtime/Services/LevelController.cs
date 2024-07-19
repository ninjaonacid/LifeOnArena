using System;
using System.Threading;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.EventSystem;
using Code.Runtime.CustomEvents;
using Code.Runtime.Logic.WaveLogic;
using Code.Runtime.Modules.LocalizationProvider;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
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
        private readonly LocalizationService _localService;
        private readonly IGameDataContainer _gameData;
        private readonly ISaveLoadService _saveLoad;

        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        public LevelController(EnemySpawnerController spawnerController, ScreenService screenService,
            IEventSystem eventSystem, PlayerControls controls,
            LevelLoader levelLoader, IGameDataContainer gameData, LocalizationService localService,
            ISaveLoadService saveLoad)
        {
            _spawnerController = spawnerController;
            _screenService = screenService;
            _eventSystem = eventSystem;
            _controls = controls;
            _levelLoader = levelLoader;
            _gameData = gameData;
            _localService = localService;
            _saveLoad = saveLoad;
        }

        public void Initialize()
        {
            _spawnerController.WaveCleared += WaveCleared;
            _spawnerController.CommonEnemiesCleared += CommonEnemiesCleared;
            _spawnerController.BossKilled += BossKilled;
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
            var localizedString = _localService.GetLocalizedString("ReturnToPortal");
            _screenService.Open(ScreenID.MessageWindow, new TimerMessageDto(localizedString, _timerToEndOfLevel ));
            await UniTask.Delay(TimeSpan.FromSeconds(_timerToEndOfLevel));
            _saveLoad.SaveData();
            _levelLoader.LoadLevel("MainMenu");
        }

        private void BossKilled()
        {
            Debug.Log("BOSS KILLED EVENT CALLED");
        }

        private void CommonEnemiesCleared(int secondsToBoss)
        {
            var levelConfig = _levelLoader.GetCurrentLevelConfig();
            
            if (levelConfig.IsBossLevel)
            {
                var localizedString = _localService.GetLocalizedString("BossSpawnMessage");
                _screenService.Open(ScreenID.MessageWindow, new TimerMessageDto(localizedString, _spawnerController.TimeToNextWave));
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
            var localizedString = _localService.GetLocalizedString("WaveSpawnMessage");
            _screenService.Open(ScreenID.MessageWindow, new TimerMessageDto(localizedString, _spawnerController.TimeToNextWave));
        }

        private async void HeroDead(HeroDeadEvent obj)
        {
            _controls.Player.Disable();

            var localizedString = _localService.GetLocalizedString("YouAreDead");
            
            _screenService.Open(ScreenID.MessageWindow, new MessageDto(localizedString));
            
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
