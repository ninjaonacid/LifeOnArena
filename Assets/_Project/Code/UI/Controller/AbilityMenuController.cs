using Code.UI.Model;
using Code.UI.Services;
using Code.UI.SkillsMenu;
using Code.UI.View;
using UniRx;
using UnityEngine;

namespace Code.UI.Controller
{
    public class AbilityMenuController : IScreenController
    {
        private AbilityMenuModel _model;
        private AbilityMenuView _view;
        private IScreenService _screenService;

        public void InitController(IScreenModel model, BaseView view, IScreenService screenService)
        {
            _model = model as AbilityMenuModel;
            _view = view as AbilityMenuView;
            _screenService = screenService;

            _view.CloseButton.OnClickAsObservable().Subscribe(x => _screenService.Close(_view.ScreenId));
        }
    }
}