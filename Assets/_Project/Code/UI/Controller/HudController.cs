using Code.UI.Model;
using Code.UI.Services;
using Code.UI.View;
using Code.UI.View.HUD;
using UnityEngine;

namespace Code.UI.Controller
{
    public class HudController : IScreenController
    {
        private HudModel _model;
        private HudView _view;
        public void InitController(IScreenModel model, BaseView view, IScreenService screenService)
        {
            _model = model as HudModel;
            _view = view as HudView;
            
           
            
        }
    }
}
