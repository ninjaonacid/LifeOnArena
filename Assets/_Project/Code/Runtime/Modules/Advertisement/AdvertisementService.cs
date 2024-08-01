using System;
using Code.Runtime.ConfigData;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Config;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PauseService;
using Code.Runtime.Services.PersistentProgress;
using GamePush;
using VContainer.Unity;

namespace Code.Runtime.Modules.Advertisement
{
    public class AdvertisementService : IInitializable, IDisposable
    {

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

        public void ShowReward(Action onRewardedStart, Action<string> onRewarded, Action<bool> onRewardedClose)
        {
            GP_Ads.ShowRewarded(onRewardedStart: onRewardedStart,
                onRewardedReward: onRewarded,
                onRewardedClose: onRewardedClose);
        }

        public void ShowInterstitial() => GP_Ads.ShowFullscreen(OnInterstitialStart, OnInterstitialClosed);

        public void Initialize()
        {
            _adsConfig = _configProvider.GetAdsConfig();
        }

        private void OnInterstitialStart()
        {
            _playerControls.Disable();
            _pauseService.PauseGame();
            _audioService.MuteAll(true);
        }

        private void OnInterstitialClosed(bool success)
        {
            _audioService.MuteMusic(!_gameData.AudioData.isMusicOn);
            _audioService.MuteSounds(!_gameData.AudioData.isSoundOn);
            
            if (_levelLoader.GetCurrentLevelConfig().LevelId.Name != "MainMenu")
            {
                _playerControls.Enable();
            }

            _pauseService.UnpauseGame();
        }

        public void Dispose()
        {
        }
    }
}