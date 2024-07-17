using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.MissionSummaryWindowModel;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.MissionSummaryWindow;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class MissionSummaryWindowController : IScreenController
    {
        private MissionSummaryWindowModel _model;
        private MissionSummaryWindowView _view;
        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _view = windowView as MissionSummaryWindowView;
            _model = model as MissionSummaryWindowModel;
            
            Assert.IsNotNull(_view);
            Assert.IsNotNull(_model);
            
        }
    }
}
