using System;
using Code.Services.SaveLoad;
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
        private readonly ISaveLoadService _saveLoad;

        public AbilityMenuController(ISaveLoadService saveLoad)
        {
            _saveLoad = saveLoad;
        }

        public void InitController(IScreenModel model, BaseView view, IScreenService screenService)
        {
            _model = model as AbilityMenuModel;
            _view = view as AbilityMenuView;
            _screenService = screenService;

            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);
            
            _model.LoadData();
            
            _view.AbilityContainer.InitializeAbilityContainer(_model.GetSlots().Count);

            _view.CloseButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    _model.SaveModelData();
                    _screenService.Close(_view.ScreenId);
                });

            _view.AbilityContainer.OnAbilitySelected += AbilitySelected;
            _view.EquipButton.OnButtonPressed += Equip;
            _view.UnEquipButton.OnButtonPressed += UnEquip;
            
          UpdateData();
        }

        private void UpdateData()
        {
            var abilityList = _model.GetSlots();
            
            for (var index = 0; index < abilityList.Count; index++)
            {
                var ability = abilityList[index];
                int equippedIndex = 0;
                if (ability.IsEquipped)
                {
                    equippedIndex = _model.GetEquippedSlotIndex(ability);
                }
                _view.AbilityContainer.UpdateData(index,  equippedIndex,  ability.Ability.Icon, ability.IsUnlocked);
            }
        }

        private void AbilitySelected(int abilityIndex)
        {
            var isEquipped = _model.IsAbilityEquipped(abilityIndex);
            var isUnlocked = _model.IsAbilityUnlocked(abilityIndex);
            
            _view.EquipButton.ShowButton(!isEquipped && isUnlocked);
            _view.UnEquipButton.ShowButton(isEquipped && isUnlocked);
            _view.UnlockButton.ShowButton(!isEquipped && !isUnlocked);
        }

        private void UnEquip()
        {
            var abilityIndex = _view.AbilityContainer.GetSelectedSlotIndex();
            
            _model.UnEquipAbility(abilityIndex);
            AbilitySelected(abilityIndex);
            
            UpdateData();
        }

        private void Equip()
        {
            var abilityIndex = _view.AbilityContainer.GetSelectedSlotIndex();
            
            _model.EquipAbility(abilityIndex);
            
            AbilitySelected(abilityIndex);
            
            UpdateData();
        }

        public void Dispose()
        {
            _view.AbilityContainer.OnAbilitySelected -= AbilitySelected;
            _view.EquipButton.OnButtonPressed -= Equip;
            _view.UnEquipButton.OnButtonPressed -= UnEquip;
        }
    }
}
