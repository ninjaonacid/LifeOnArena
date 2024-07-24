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
        private readonly ScreenService _screenService;

        private readonly CompositeDisposable _disposable;
        private readonly CancellationTokenSource _cts = new();
        public HeroDeathPopupController(LevelLoader levelLoader, AdvertisementService adService,
            PauseService pauseService, HeroFactory heroFactory)
        {
            _levelLoader = levelLoader;
            _adService = adService;
            _pauseService = pauseService;
            _heroFactory = heroFactory;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as HeroDeathPopupModel;
            _view = windowView as HeroDeathPopupView;

            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);

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
        }

        private async UniTask WaitForAdTask(CancellationToken token)
        {
            await UniTask.WaitUntil(() => _adService.RewardedState == RewardedState.Rewarded, cancellationToken: token);
            HandleReviveHero();
        }
        private void HandleReviveHero()
        {
            var playerInitialPoint = _levelLoader.GetCurrentLevelConfig().HeroInitialPosition;
            var heroHealth = _heroFactory.HeroGameObject.GetComponent<HeroHealth>();
            var heroDeath = _heroFactory.HeroGameObject.GetComponent<HeroDeath>();
            
            heroHealth.Health.ResetHealth();
            heroDeath.Revive();
            
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cts.Cancel();
        }
    }
}