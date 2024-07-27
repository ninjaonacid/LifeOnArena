using System;
using Code.Runtime.ConfigData;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Config;
using Code.Runtime.Logic.Timer;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PauseService;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using VContainer.Unity;

namespace Code.Runtime.Modules.Advertisement
{
    public class AdvertisementService : IInitializable, IDisposable
    {
        public RewardedState RewardedState => Bridge.advertisement.rewardedState;
        public InterstitialState InterstitialState => Bridge.advertisement.interstitialState;

        private readonly AudioService _audioService;
        private readonly PauseService _pauseService;
        private readonly PlayerControls _playerControls;
        private readonly ConfigProvider _configProvider;
        private readonly LevelLoader _levelLoader;
        
        private AdvertisementConfig _adsConfig;

        public AdvertisementService(AudioService audioService, PauseService pauseService, PlayerControls playerControls,
            ConfigProvider configProvider, LevelLoader levelLoader)
        {
            _audioService = audioService;
            _pauseService = pauseService;
            _playerControls = playerControls;
            _configProvider = configProvider;
            _levelLoader = levelLoader;
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
            Bridge.advertisement.rewardedStateChanged += OnRewardedStateChange;
            Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChange;
        }

        private void OnInterstitialStateChange(InterstitialState state)
        {
            switch (state)
            {
                case InterstitialState.Opened:
                    _playerControls.Disable();
                    _pauseService.PauseGame();
                    _audioService.PauseAll();
                    break;
                case InterstitialState.Closed:
                    _audioService.UnpauseAll();
                    if (_levelLoader.GetCurrentLevelConfig().LevelId.Name != "MainMenu")
                    {
                        _playerControls.Enable();
                    }
                    _pauseService.UnpauseGame();
                    break;
                case InterstitialState.Failed:
                    _audioService.UnpauseAll();
                    if (_levelLoader.GetCurrentLevelConfig().LevelId.Name != "MainMenu")
                    {
                        _playerControls.Enable();
                    }
                    _pauseService.UnpauseGame();
                    break;
            }
        }

        private void OnRewardedStateChange(RewardedState state)
        {
            switch (state)
            {
                case RewardedState.Opened:
                    _pauseService.PauseGame();
                    _audioService.PauseAll();
                    break;
                case RewardedState.Closed:
                    _audioService.UnpauseAll();
                    _pauseService.UnpauseGame();
                  
                    break;
                case RewardedState.Failed:
                    _audioService.UnpauseAll();
                    _pauseService.UnpauseGame();
                    break;
            }
        }

        public void Dispose()
        {
        }
    }
}