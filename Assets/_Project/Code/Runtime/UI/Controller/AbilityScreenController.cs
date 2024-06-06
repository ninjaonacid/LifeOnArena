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
    public class AbilityScreenController : IScreenController, IDisposable
    {
        private AbilityScreenModel _model;
        private AbilityScreenView _screenView;
        private ScreenService _screenService;
        private readonly ISaveLoadService _saveLoad;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        public AbilityScreenController(ISaveLoadService saveLoad)
        {
            _saveLoad = saveLoad;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as AbilityScreenModel;
            _screenView = windowView as AbilityScreenView;
            _screenService = screenService;

            Assert.IsNotNull(_model);
            Assert.IsNotNull(_screenView);
            
            _model.LoadData();

            _screenView.AbilityContainer.Initialize();

            _screenView.CloseButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    _model.SaveModelData();
                    _screenService.Close(this);
                });
            
            _screenView.AbilityContainer
                .OnAbilitySelectedAsObservable()
                .Subscribe(AbilitySelected)
                .AddTo(_disposable);
            
            _screenView.EquipButton
                .OnClickAsObservable()
                .Subscribe(x => Equip())
                .AddTo(_disposable);
            
            _screenView.UnEquipButton
                .OnClickAsObservable()
                .Subscribe(x => UnEquip())
                .AddTo(_disposable);
            
            _screenView.UnlockButton
                .OnClickAsObservable()
                .Subscribe(x => Unlock())
                .AddTo(_disposable);

            _screenView.ResourcesCount.ChangeText(_model.Souls.Value.ToString());
            _model.Souls.Subscribe(x => _screenView.ResourcesCount.ChangeText(_model.Souls.Value.ToString()));
            
            
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
                _screenView.AbilityContainer.UpdateData(index,  equippedIndex,  ability.Icon, ability.IsUnlocked);
            }
        }

        private void AbilitySelected(int abilityIndex)
        {
            var isEquipped = _model.IsAbilityEquipped(abilityIndex);
            var isUnlocked = _model.IsAbilityUnlocked(abilityIndex);
            
            _screenView.EquipButton.ShowButton(!isEquipped && isUnlocked);
            _screenView.UnEquipButton.ShowButton(isEquipped && isUnlocked);
            _screenView.UnlockButton.ShowButton(!isEquipped && !isUnlocked);

            var slotModel = _model.GetSlotByIndex(abilityIndex);

            _screenView.AbilityDescription.Icon.sprite = slotModel.Icon;
            _screenView.AbilityDescription.Text.text = slotModel.Description;
        }

        private void Unlock()
        {
            if (_screenView.AbilityContainer.TryGetSelectedSlotIndex(out var index))
            {
                _model.UnlockAbility(index);
                AbilitySelected(index);
            };

            UpdateData();
        }

        private void UnEquip()
        {
            if (_screenView.AbilityContainer.TryGetSelectedSlotIndex(out var index))
            {
                _model.UnEquipAbility(index);
                AbilitySelected(index);
            };

            UpdateData();
        }

        private void Equip()
        {
            if (_screenView.AbilityContainer.TryGetSelectedSlotIndex(out var index))
            {
                _model.EquipAbility(index);
                AbilitySelected(index);
            };

            UpdateData();
        }

        public void Dispose()
        {
            _disposable.Dispose();
            
        }
    }
}
