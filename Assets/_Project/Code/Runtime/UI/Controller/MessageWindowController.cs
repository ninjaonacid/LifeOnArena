using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.MessageWindow;
using UnityEngine;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class MessageWindowController : IScreenController
    {
        private MessageWindowModel _model;
        private MessageWindowView _view;
        public void InitController(IScreenModel model, BaseView view, ScreenService screenService)
        {
            _model = model as MessageWindowModel;
            _view = view as MessageWindowView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);

            _view.Text.text = _model.Message;

        }
    }
}
