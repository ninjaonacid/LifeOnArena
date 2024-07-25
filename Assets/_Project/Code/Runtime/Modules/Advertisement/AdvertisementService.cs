using System;
using Code.Runtime.Core.Audio;
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
        public AdvertisementService(AudioService audioService, PauseService pauseService, PlayerControls controls)
        {
            _audioService = audioService;
            _pauseService = pauseService;
            _playerControls = controls;
        }

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
                    _playerControls.Enable();
                    _pauseService.UnpauseGame();
                    break;
                case InterstitialState.Failed:
                    _audioService.UnpauseAll();
                    _playerControls.Enable();
                    _pauseService.UnpauseGame();
                    break;
            }
        }

        private void OnRewardedStateChange(RewardedState state)
        {
            switch (state)
            {
                case RewardedState.Opened:
                    _playerControls.Disable();
                    _pauseService.PauseGame();
                    _audioService.PauseAll();
                    break;
                case RewardedState.Closed:
                    _audioService.UnpauseAll();
                    _pauseService.UnpauseGame();
                    _playerControls.Enable();
                    break;
                case RewardedState.Failed:
                    _audioService.UnpauseAll();
                    _pauseService.UnpauseGame();
                    _playerControls.Enable();
                    break;
            }
        }

   

        public void Dispose()
        {
        }
    }
}
