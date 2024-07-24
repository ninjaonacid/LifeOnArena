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
        public RewardedState RewardedState;
        private readonly AudioService _audioService;
        private readonly PauseService _pauseService;
        public AdvertisementService(AudioService audioService, PauseService pauseService)
        {
            _audioService = audioService;
            _pauseService = pauseService;
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
            if (state == InterstitialState.Opened)
            {
                _audioService.PauseAll();
            } 
            else if (state == InterstitialState.Closed)
            {
                _audioService.UnpauseAll();
            }
            else if (state == InterstitialState.Failed)
            {
                _audioService.UnpauseAll();
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
            } 
            else if (state == RewardedState.Failed)
            {
                _audioService.UnpauseAll();
                _pauseService.UnpauseGame();
            } 
            else if (state == RewardedState.Rewarded)
            {
                _audioService.UnpauseAll();
                _pauseService.UnpauseGame();
            }
        }

   

        public void Dispose()
        {
        }
    }
}
