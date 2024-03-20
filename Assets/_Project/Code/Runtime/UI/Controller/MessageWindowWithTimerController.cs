using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;

namespace Code.Runtime.UI.Controller
{
    public class MessageWindowWithTimerController : IScreenController
    {
        private MessageWindowModelWithTimer _model;
        private MessageWindowWithTimerView _view;
        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as MessageWindowModelWithTimer;
            _view = windowView as MessageWindowWithTimerView;
            
        }
    }
}