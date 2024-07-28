using System;
using Code.Runtime.ConfigData;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Config;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PauseService;
using Code.Runtime.Services.PersistentProgress;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using UnityEditor;
using UnityEngine;
using VContainer.Unity;

namespace Code.Runtime.Modules.Advertisement
{
    public class AdvertisementService : IInitializable, IDisposable
    {
        public event Action<RewardedState> OnRewardedStateChange;

        private readonly AudioService _audioService;
        private readonly PauseService _pauseService;
        private readonly PlayerControls _playerControls;
        private readonly ConfigProvider _configProvider;
        private readonly LevelLoader _levelLoader;
        private readonly IGameDataContainer _gameData;
        private AdvertisementConfig _adsConfig;

        public AdvertisementService(AudioService audioService, PauseService pauseService, PlayerControls playerControls,
            ConfigProvider configProvider, LevelLoader levelLoader, IGameDataContainer gameData)
        {
            _audioService = audioService;
            _pauseService = pauseService;
            _playerControls = playerControls;
            _configProvider = configProvider;
            _levelLoader = levelLoader;
            _gameData = gameData;
        }

        public int ReviveRewardsPossible() => _adsConfig.HeroRevivePerLevel;

        public void ShowReward()
        {
            Bridge.advertisement.ShowRewarded();
           
        }

        public void ShowInterstitial()
        {
            Bridge.advertisement.ShowInterstitial();
        }

        public void Initialize()
        {
            _adsConfig = _configProvider.GetAdsConfig();
            Bridge.advertisement.rewardedStateChanged += HandleRewardedStateChange;
            Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChange;
        }

        private void OnInterstitialStateChange(InterstitialState state)
        {
            switch (state)
            {
                case InterstitialState.Opened:
                    _playerControls.Disable();
                    _pauseService.PauseGame();
                    _audioService.MuteAll(true);
                    break;
                case InterstitialState.Closed:
                    
                    _audioService.MuteMusic(!_gameData.AudioData.isMusicOn);
                    _audioService.MuteSounds(!_gameData.AudioData.isSoundOn);
                    if (_levelLoader.GetCurrentLevelConfig().LevelId.Name != "MainMenu")
                    {
                        _playerControls.Enable();
                    }

                    _pauseService.UnpauseGame();
                    break;
                case InterstitialState.Failed:
                    _audioService.MuteMusic(!_gameData.AudioData.isMusicOn);
                    _audioService.MuteSounds(!_gameData.AudioData.isSoundOn);
                    if (_levelLoader.GetCurrentLevelConfig().LevelId.Name != "MainMenu")
                    {
                        _playerControls.Enable();
                    }

                    _pauseService.UnpauseGame();
                    break;
            }
        }

        private void HandleRewardedStateChange(RewardedState state)
        {
            switch (state)
            {
                case RewardedState.Loading:
                    OnRewardedStateChange?.Invoke(state);
                    break;
                case RewardedState.Opened:
                    OnRewardedStateChange?.Invoke(state);
                    break;
                case RewardedState.Failed:
                    OnRewardedStateChange?.Invoke(state);
                    break;
                case RewardedState.Closed:
                    OnRewardedStateChange?.Invoke(state);
                    break;
                case RewardedState.Rewarded:
                    OnRewardedStateChange?.Invoke(state);
                    break;
                
                default:
                    break;
            }
        }
        

        public void Dispose()
        {
        }
    }
}