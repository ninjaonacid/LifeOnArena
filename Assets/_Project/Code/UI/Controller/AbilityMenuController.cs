using System;
using Code.UI.Model;
using Code.UI.Model.AbilityMenu;
using Code.UI.Services;
using Code.UI.View;
using Code.UI.View.AbilityMenu;
using UniRx;
using UnityEngine.Assertions;

namespace Code.UI.Controller
{
    public class AbilityMenuController : IScreenController, IDisposable
    {
        private AbilityMenuModel _model;
        private AbilityMenuView _view;
        private IScreenService _screenService;

        public void InitController(IScreenModel model, BaseView view, IScreenService screenService)
        {
            _model = model as AbilityMenuModel;
            _view = view as AbilityMenuView;
            _screenService = screenService;

            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);
            
            _view.AbilityContainer.InitializeAbilityContainer(_model.GetSlots().Count);

            _view.CloseButton
                .OnClickAsObservable()
                .Subscribe(x => _screenService.Close(_view.ScreenId));

            _view.AbilityContainer.OnAbilitySelected += AbilitySelected;
            _view.EquipButton.OnEquipButtonPressed += Equip;
            _view.UnEquipButton.OnUnEquipButtonPressed += UnEquip;

          UpdateData();
        }

        private void UpdateData()
        {
            var list = _model.GetSlots();
            
            for (var index = 0; index < list.Count; index++)
            {
                var ability = list[index];
                _view.AbilityContainer.UpdateData(index, ability.Ability.Icon);
            }
        }

        private void AbilitySelected(int abilityIndex)
        {
            var slotModel = _model.GetSlots();
            
            if (slotModel[abilityIndex].IsEquipped)
            {
                _view.EquipButton.gameObject.SetActive(true);
            }
            else
            {
                _view.UnEquipButton.gameObject.SetActive(false);
            }
        }

        private void UnEquip()
        {
            var abilityIndex = _view.AbilityContainer.GetSelectedSlotIndex();
            
            _model.UnEquipAbility(abilityIndex);
            
        }

        private void Equip()
        {
            var abilityIndex = _view.AbilityContainer.GetSelectedSlotIndex();
            
            _model.EquipAbility(abilityIndex);
        }

        public void Dispose()
        {
            _view.AbilityContainer.OnAbilitySelected -= AbilitySelected;
            _view.EquipButton.OnEquipButtonPressed -= Equip;
            _view.UnEquipButton.OnUnEquipButtonPressed -= UnEquip;
        }
    }
}
