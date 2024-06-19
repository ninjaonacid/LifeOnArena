using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.DTO;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.WeaponRewardPopup;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class RewardPopupController : IScreenController
    {
        private RewardPopupModel _model;
        private RewardPopupView _view;

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as RewardPopupModel;
            _view = windowView as RewardPopupView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);

            if (_model.RewardDto is WeaponRewardDto weaponRewardDto)
            {
                _view.WeaponIcon.sprite = weaponRewardDto.WeaponData.WeaponIcon;
                _view.WeaponName.text = weaponRewardDto.WeaponData.name;
            } 

            _view.ClaimButton.OnClickAsObservable().Subscribe(x => screenService.Close(this));
        }
        
    }
}