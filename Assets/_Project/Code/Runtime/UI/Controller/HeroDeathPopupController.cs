using System;
using System.Threading;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PauseService;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.HeroDeathPopupView;
using Cysharp.Threading.Tasks;
using InstantGamesBridge.Modules.Advertisement;
using UniRx;
using UnityEngine;
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
        private ScreenService _screenService;

        private readonly CompositeDisposable _disposable = new();
        private readonly CancellationTokenSource _cts = new();
        public HeroDeathPopupController(LevelLoader levelLoader, AdvertisementService adService,
            PauseService pauseService, HeroFactory heroFactory, PlayerControls playerControls)
        {
            _levelLoader = levelLoader;
            _adService = adService;
            _pauseService = pauseService;
            _heroFactory = heroFactory;
            _playerControls = playerControls;
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
                .Subscribe(x => _levelLoader.LoadLevel("MainMenu")).AddTo(_disposable);

            _view.RewardRessurectButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    _adService.ShowReward();
                    WaitForAdTask(_cts.Token).Forget();
                }).AddTo(_disposable);
            
            var heroDeath = _heroFactory.HeroGameObject.GetComponent<HeroDeath>();

            if (heroDeath.RevivedNumber >= 1)
            {
                _view.RewardRessurectButton.Show(false);
                _view.ReturnToPortalButton.PlayScaleAnimation();
            }
        }

        private async UniTask WaitForAdTask(CancellationToken token)
        {
            await UniTask.WaitUntil(() => _adService.RewardedState != RewardedState.Loading, cancellationToken: token);
            HandleAdResult(_adService.RewardedState);
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
            _playerControls.Player.Enable();
            
            _screenService.Close(this);
        }
        
        private void HandleAdResult(RewardedState state)
        {
            switch (state)
            {
                case RewardedState.Rewarded:
                    HandleReviveHero();
                    break;
                case RewardedState.Failed:
                    _levelLoader.LoadLevel("MainMenu");
                    break;
                case RewardedState.Closed:
                case RewardedState.Opened:
                    break;
                default:
                    Debug.LogWarning($"Unexpected RewardedState: {state}");
                    _screenService.Close(this);
                    break;
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cts.Cancel();
        }
    }
}