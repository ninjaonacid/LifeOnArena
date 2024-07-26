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
using TimerMessageDto = Code.Runtime.UI.Model.DTO.TimerMessageDto;

namespace Code.Runtime.Services
{
    public class LevelController : IInitializable, IDisposable
    {
        private readonly EnemySpawnerController _spawnerController;
        private readonly ScreenService _screenService;
        private readonly IEventSystem _eventSystem;
        private readonly LevelLoader _levelLoader;
        private readonly PlayerControls _controls;
        private readonly LocalizationService _localService;
        private readonly IGameDataContainer _gameData;
        private readonly ISaveLoadService _saveLoad;
        private readonly CancellationTokenSource _cancellationToken = new();

        public LevelController(EnemySpawnerController spawnerController, 
            ScreenService screenService,
            IEventSystem eventSystem, PlayerControls controls,
            LevelLoader levelLoader, IGameDataContainer gameData, 
            LocalizationService localService,
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
            SubscribeToEvents();
            _controls.LevelControls.Enable();
        }

        private void SubscribeToEvents()
        {
            _eventSystem.Subscribe<HeroDeadEvent>(HeroDeadEvent);
            _spawnerController.WaveStart += WaveStart;
            _spawnerController.CommonEnemiesCleared += CommonEnemiesCleared;
            _spawnerController.BossKilled += BossKilled;
            _spawnerController.BossSpawned += OnBossSpawn;
        }

        private void OnBossSpawn(GameObject go, MobIdentifier mobId)
        {
            _eventSystem.FireEvent(new BossSpawnEvent(go, mobId));
        }

        private void LevelEnd()
        {
            var currentLevel = _levelLoader.GetCurrentLevelConfig();
            _gameData.PlayerData.WorldData.LocationProgressData.CompletedLocations.Add(currentLevel.LevelId.Id);
            _saveLoad.SaveData();
            
            _eventSystem.FireEvent(new LevelEndEvent());
        }

        private void BossKilled()
        {
            LevelEndTask().Forget();
        }
        
        private void CommonEnemiesCleared(int secondsToBoss)
        {
            var levelConfig = _levelLoader.GetCurrentLevelConfig();

            if (levelConfig.IsBossLevel)
            {
                var localizedString = _localService.GetLocalizedString("BossSpawnMessage");
                _screenService.Open(ScreenID.MessageWindow,
                    new TimerMessageDto(localizedString, _spawnerController.TimeToNextWave));
            }
            else
            {
                LevelEndTask().Forget();
            }
        }

        private async UniTaskVoid LevelEndTask()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(7));
            LevelEnd();
        }

        private void WaveStart(int secondsToNextWave)
        {
            var localizedString = _localService.GetLocalizedString("WaveSpawnMessage");
            _screenService.Open(ScreenID.MessageWindow,
                new TimerMessageDto(localizedString, _spawnerController.TimeToNextWave));
        }

        private async UniTaskVoid HeroDead(HeroDeadEvent obj)
        {
            _controls.Player.Disable();

            await UniTask.Delay(TimeSpan.FromSeconds(1),
                DelayType.DeltaTime,
                PlayerLoopTiming.Update, _cancellationToken.Token);
            
            _screenService.Open(ScreenID.HeroDeathPopupView);
        }

        private void HeroDeadEvent(HeroDeadEvent obj)
        { 
            HeroDead(obj).Forget();
        }

        private void UnsubscribeFromEvents()
        {
            _eventSystem.Unsubscribe<HeroDeadEvent>(HeroDeadEvent);
            _spawnerController.WaveStart -= WaveStart;
            _spawnerController.CommonEnemiesCleared -= CommonEnemiesCleared;
            _spawnerController.BossKilled -= BossKilled;
            _spawnerController.BossSpawned -= OnBossSpawn;
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
        }
    }
}