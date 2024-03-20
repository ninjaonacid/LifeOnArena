using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.MessageWindow;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class MessageWindowController : IScreenController
    {
        private MessageWindowModel _model;
        private MessageWindowWindowView _windowView;
        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as MessageWindowModel;
            _windowView = windowView as MessageWindowWindowView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_windowView);

            _windowView.Text.text = _model.Message;

        }
    }
}
