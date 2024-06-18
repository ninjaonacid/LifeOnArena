using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.WeaponRewardPopup;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class WeaponRewardPopupController : IScreenController
    {
        private WeaponRewardPopupModel _model;
        private WeaponRewardPopupView _view;

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as WeaponRewardPopupModel;
            _view = windowView as WeaponRewardPopupView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);
            
            
        }
    }
}