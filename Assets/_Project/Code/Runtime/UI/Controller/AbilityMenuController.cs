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
        private AbilityMenuWindowView _windowView;
        private ScreenService _screenService;
        private readonly ISaveLoadService _saveLoad;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        public AbilityMenuController(ISaveLoadService saveLoad)
        {
            _saveLoad = saveLoad;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as AbilityMenuModel;
            _windowView = windowView as AbilityMenuWindowView;
            _screenService = screenService;

            Assert.IsNotNull(_model);
            Assert.IsNotNull(_windowView);
            
            _model.LoadData();
            
            _windowView.AbilityContainer.InitializeAbilityContainer(_model.GetSlots().Count);

            _windowView.CloseButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    _model.SaveModelData();
                    _screenService.Close(this);
                });
            
            _windowView.AbilityContainer
                .OnAbilitySelectedAsObservable()
                .Subscribe(AbilitySelected)
                .AddTo(_disposable);
            
            _windowView.EquipButton
                .OnClickAsObservable()
                .Subscribe(x => Equip())
                .AddTo(_disposable);
            
            _windowView.UnEquipButton
                .OnClickAsObservable()
                .Subscribe(x => UnEquip())
                .AddTo(_disposable);
            
            _windowView.UnlockButton
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
                _windowView.AbilityContainer.UpdateData(index,  equippedIndex,  ability.ActiveAbilityBlueprintBase.Icon, ability.IsUnlocked);
            }
        }

        private void AbilitySelected(int abilityIndex)
        {
            var isEquipped = _model.IsAbilityEquipped(abilityIndex);
            var isUnlocked = _model.IsAbilityUnlocked(abilityIndex);
            
            _windowView.EquipButton.ShowButton(!isEquipped && isUnlocked);
            _windowView.UnEquipButton.ShowButton(isEquipped && isUnlocked);
            _windowView.UnlockButton.ShowButton(!isEquipped && !isUnlocked);
        }

        private void Unlock()
        {
            var abilityIndex = _windowView.AbilityContainer.GetSelectedSlotIndex();
            _model.UnlockAbility(abilityIndex);
            
            AbilitySelected(abilityIndex);
            UpdateData();
        }

        private void UnEquip()
        {
            var abilityIndex = _windowView.AbilityContainer.GetSelectedSlotIndex();
            
            _model.UnEquipAbility(abilityIndex);
            AbilitySelected(abilityIndex);
            
            UpdateData();
        }

        private void Equip()
        {
            var abilityIndex = _windowView.AbilityContainer.GetSelectedSlotIndex();
            
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
