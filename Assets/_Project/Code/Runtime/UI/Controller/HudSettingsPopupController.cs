using Code.Runtime.Services.PauseService;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.HudSettingsPopupView;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class HudSettingsPopupController : IScreenController
    {
        private HudSettingsPopupView _view;
        private HudSettingsPopupModel _model;

        private readonly PauseService _pauseService;

        public HudSettingsPopupController(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _view = windowView as HudSettingsPopupView;
            _model = model as HudSettingsPopupModel;
            
            Assert.IsNotNull(_view);
            Assert.IsNotNull(_model);
            
            _view.SoundButton.SetButton(_model.IsSoundOn);
            _view.MusicButton.SetButton(_model.IsMusicOn);

            _view.CloseButton.OnClickAsObservable()
                .Subscribe(x =>
            {
                _pauseService.UnpauseGame();
                screenService.Close(this);
            });
        }
    }
}