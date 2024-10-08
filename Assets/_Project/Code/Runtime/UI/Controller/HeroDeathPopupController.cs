﻿using System;
using System.Threading;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.LevelLoaderService;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Services.PauseService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.HeroDeathPopupView;
using GamePush;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class HeroDeathPopupController : IScreenController, IDisposable
    {
        private HeroDeathPopupModel _model;
        private HeroDeathPopupView _view;

        private readonly LevelLoader _levelLoader;
        private readonly AdvertisementService _adService;
        private readonly PauseService _pauseService;
        private readonly HeroFactory _heroFactory;
        private readonly PlayerControls _playerControls;
        private readonly AudioService _audioService;
        private readonly IGameDataContainer _gameData;
        private ScreenService _screenService;

        private readonly CompositeDisposable _disposable = new();
        private readonly CancellationTokenSource _cts = new();

        public HeroDeathPopupController(LevelLoader levelLoader, AdvertisementService adService,
            PauseService pauseService, HeroFactory heroFactory, PlayerControls playerControls,
            AudioService audioService, IGameDataContainer gameData)
        {
            _levelLoader = levelLoader;
            _adService = adService;
            _pauseService = pauseService;
            _heroFactory = heroFactory;
            _playerControls = playerControls;
            _audioService = audioService;
            _gameData = gameData;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as HeroDeathPopupModel;
            _view = windowView as HeroDeathPopupView;

            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);

            _screenService = screenService;

            _view.ReturnToPortalButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    _adService.ShowInterstitial();
                    _levelLoader.LoadLevel("MainMenu");
                }).AddTo(_disposable);

            _view.RewardRessurectButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    _adService.ShowReward(StartAd, Rewarded, Closed);
                    
                }).AddTo(_disposable);

            var heroDeath = _heroFactory.HeroGameObject.GetComponent<HeroDeath>();

            if (heroDeath.RevivedNumber >= _adService.ReviveRewardsPossible())
            {
                _view.RewardRessurectButton.Show(false);
                _view.ReturnToPortalButton.PlayScaleAnimation();
            }
        }

        private void StartAd()
        {
            _pauseService.PauseGame();
            _audioService.MuteSounds(true);
            _audioService.MuteMusic(true);
            GP_Game.GameplayStop();
        }

        private void Rewarded(string rewardId)
        {
            HandleReviveHero();
            
        }

        private void Closed(bool success)
        {
            _audioService.MuteMusic(!_gameData.AudioData.isMusicOn);
            _audioService.MuteSounds(!_gameData.AudioData.isSoundOn);
            _pauseService.UnpauseGame();
            
            if (!success)
            {
                _levelLoader.LoadLevel("MainMenu");
            }
            
            _screenService.Close(this);
            GP_Game.GameplayStart();
        }
        private void HandleReviveHero()
        {
            var playerInitialPoint = _levelLoader.GetCurrentLevelConfig().HeroInitialPosition;
            var heroHealth = _heroFactory.HeroGameObject.GetComponent<HeroHealth>();
            var heroDeath = _heroFactory.HeroGameObject.GetComponent<HeroDeath>();
            var heroMovement = _heroFactory.HeroGameObject.GetComponent<HeroMovement>();

            heroMovement.Warp(playerInitialPoint);
            heroHealth.Health.ResetHealth();
            heroDeath.Revive();
            _playerControls.Enable();
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cts.Cancel();
        }
    }
}