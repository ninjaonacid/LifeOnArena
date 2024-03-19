using System;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.AbilityMenu;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.AbilityMenu;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class AbilityMenuController : IScreenController, IDisposable
    {
        private AbilityMenuModel _model;
        private AbilityMenuView _view;
        private ScreenService _screenService;
        private readonly ISaveLoadService _saveLoad;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        public AbilityMenuController(ISaveLoadService saveLoad)
        {
            _saveLoad = saveLoad;
        }

        public void InitController(IScreenModel model, BaseView view, ScreenService screenService)
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
            
            _view.AbilityContainer
                .OnAbilitySelectedAsObservable()
                .Subscribe(AbilitySelected)
                .AddTo(_disposable);
            
            _view.EquipButton
                .OnClickAsObservable()
                .Subscribe(x => Equip())
                .AddTo(_disposable);
            
            _view.UnEquipButton
                .OnClickAsObservable()
                .Subscribe(x => UnEquip())
                .AddTo(_disposable);
            
            _view.UnlockButton
                .OnClickAsObservable()
                .Subscribe(x => Unlock())
                .AddTo(_disposable);
            
            
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
                _view.AbilityContainer.UpdateData(index,  equippedIndex,  ability.ActiveAbilityBlueprintBase.Icon, ability.IsUnlocked);
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

        private void Unlock()
        {
            var abilityIndex = _view.AbilityContainer.GetSelectedSlotIndex();
            _model.UnlockAbility(abilityIndex);
            
            AbilitySelected(abilityIndex);
            UpdateData();
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
            _disposable.Dispose();
            
        }
    }
}
