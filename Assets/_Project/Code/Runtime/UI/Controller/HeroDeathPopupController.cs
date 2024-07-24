using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PauseService;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.HeroDeathPopupView;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class HeroDeathPopupController : IScreenController
    {
        private HeroDeathPopupModel _model;
        private HeroDeathPopupView _view;

        private readonly LevelLoader _levelLoader;
        private readonly AdvertisementService _adService;
        private readonly PauseService _pauseService;
        public HeroDeathPopupController(LevelLoader levelLoader, AdvertisementService adService, PauseService pauseService)
        {
            _levelLoader = levelLoader;
            _adService = adService;
            _pauseService = pauseService;
        }
        
        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as HeroDeathPopupModel;
            _view = windowView as HeroDeathPopupView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);

            _view.ReturnToPortalButton
                .OnClickAsObservable()
                .Subscribe(x => _levelLoader.LoadLevel("MainMenu"));

            _view.RewardRessurectButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    _adService.ShowReward();
                });
        }
    }
}