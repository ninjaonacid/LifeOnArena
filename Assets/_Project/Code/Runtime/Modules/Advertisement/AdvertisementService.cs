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
            
            #if !UNITY_EDITOR
            _playerControls.Disable();
            _pauseService.PauseGame();
            #endif
        }

        public void ShowInterstitial()
        {
            Bridge.advertisement.ShowInterstitial();
            
            #if !UNITY_EDITOR
            _playerControls.Disable();
            _pauseService.PauseGame();
            #endif
        }

        public void Initialize()
        {
            Bridge.advertisement.rewardedStateChanged += OnRewardedStateChange;
            Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChange;
        }

        private void OnInterstitialStateChange(InterstitialState state)
        {
            if (state == InterstitialState.Opened)
            {
                _audioService.PauseAll();
            } 
            else if (state == InterstitialState.Closed)
            {
                _audioService.UnpauseAll();
                _playerControls.Enable();
            }
            else if (state == InterstitialState.Failed)
            {
                _audioService.UnpauseAll();
                _playerControls.Enable();
            }
        }

        private void OnRewardedStateChange(RewardedState state)
        {
            if (state == RewardedState.Opened)
            {
                _audioService.PauseAll();
                _pauseService.PauseGame();
            } 
            else if (state == RewardedState.Closed)
            {
                _audioService.UnpauseAll();
                _pauseService.UnpauseGame();
                _playerControls.Enable();
            } 
            else if (state == RewardedState.Failed)
            {
                _audioService.UnpauseAll();
                _pauseService.UnpauseGame();
                _playerControls.Enable();
            } 
            else if (state == RewardedState.Rewarded)
            {
                _audioService.UnpauseAll();
                _pauseService.UnpauseGame();
                _playerControls.Enable();
            }
        }

   

        public void Dispose()
        {
        }
    }
}
