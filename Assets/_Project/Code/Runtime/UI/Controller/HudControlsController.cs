using Code.Runtime.Services.PauseService;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.ControlsHelpView;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class HudControlsController : IScreenController
    {
        private HudControlsScreenModel _model;
        private ControlsHelpScreenView _view;

        private ScreenService _screenService;
        private readonly PauseService _pauseService;

        public HudControlsController(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as HudControlsScreenModel;
            _view = windowView as ControlsHelpScreenView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);
            
            _screenService = screenService;

            _view.CloseButton.OnClickAsObservable().Subscribe(x =>
            {
                _pauseService.UnpauseGame();
                _screenService.Close(this);
            });
        }
    }
}