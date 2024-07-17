using Code.Runtime.Modules.LocalizationProvider;
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

        private readonly LocalizationService _localService;

        public RewardPopupController(LocalizationService localService)
        {
            _localService = localService;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)

        {
            _model = model as RewardPopupModel;
            _view = windowView as RewardPopupView;

            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);

            if (_model.RewardDto is WeaponRewardDto weaponRewardDto)
            {
                _view.RewardIcon.sprite = weaponRewardDto.WeaponData.WeaponIcon;
                _view.RewardName.text = weaponRewardDto.WeaponData.name;
                _view.ClaimButton.ButtonText.text = _localService.GetLocalizedString("ToInventory");
            }
            else if (_model.RewardDto is SoulRewardDto soulRewardDto)
            {
                _view.RewardIcon.sprite = soulRewardDto.Icon;
                _view.RewardName.text = soulRewardDto.Value + _localService.GetLocalizedString("SOULS");
            }

            _view.ClaimButton.OnClickAsObservable().Subscribe(x => screenService.Close(this));
            _view.ClaimButton.PlayScaleAnimation();
        }
        
    }
}