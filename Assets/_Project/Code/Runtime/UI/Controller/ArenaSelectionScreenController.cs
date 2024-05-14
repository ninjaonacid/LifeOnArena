using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.ArenaSelectionScreenModel;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.ArenaSelection;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class ArenaSelectionScreenController : IScreenController
    {
        private ArenaSelectionScreenModel _model;
        private ArenaSelectionScreenView _view;
        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as ArenaSelectionScreenModel;
            _view = windowView as ArenaSelectionScreenView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);

        }
    }
}