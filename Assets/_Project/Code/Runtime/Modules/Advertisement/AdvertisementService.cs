using System;
using Code.Runtime.Core.Audio;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using VContainer.Unity;

namespace Code.Runtime.Modules.Advertisement
{
    public class AdvertisementService : IInitializable, IDisposable
    {
        public RewardedState RewardedState;
        private AudioService _audioService;
        

        public AdvertisementService(AudioService audioService)
        {
            _audioService = audioService;
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
            } 
            else if (state == RewardedState.Closed)
            {
                _audioService.UnpauseAll();
            } 
            else if (state == RewardedState.Failed)
            {
                _audioService.UnpauseAll();
            } 
            else if (state == RewardedState.Rewarded)
            {
                
            }
        }

   

        public void Dispose()
        {
        }
    }
}
