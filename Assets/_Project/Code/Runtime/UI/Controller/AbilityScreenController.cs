using System;
using Code.Runtime.Core.Audio;
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
        protected AbilityScreenModel _model;
        protected AbilityScreenView _screenView;

        private AudioService _audioService;
        private ScreenService _screenService;
        private readonly SaveLoadService _saveLoad;
        protected readonly CompositeDisposable _disposable = new CompositeDisposable();
        public AbilityScreenController(SaveLoadService saveLoad, AudioService audioService)
        {
            _saveLoad = saveLoad;
            _audioService = audioService;
        }

        public virtual void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as AbilityScreenModel;
            _screenView = windowView as AbilityScreenView;
            _screenService = screenService;

            Assert.IsNotNull(_model);
            Assert.IsNotNull(_screenView);

            _screenView.AbilityContainer.Initialize();

            _screenView.CloseButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
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
            
            _screenView.AbilityDescription.Show(false);
            _screenView.ResourcesCount.ChangeText(_model.Souls.Value.ToString());
            _model.Souls.Subscribe(x => _screenView.ResourcesCount.ChangeText(_model.Souls.Value.ToString()));
            
            _screenView.UnlockButton.Show(false);
            _screenView.EquipButton.Show(false);
            _screenView.UnEquipButton.Show(false);
            
            
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
            _audioService.PlaySound("Select");
            
            var isEquipped = _model.IsAbilityEquipped(abilityIndex);
            var isUnlocked = _model.IsAbilityUnlocked(abilityIndex);
            
            _screenView.EquipButton.Show(!isEquipped && isUnlocked);
            _screenView.UnEquipButton.Show(isEquipped && isUnlocked);
            _screenView.UnlockButton.Show(!isEquipped && !isUnlocked);

            var slotModel = _model.GetSlotByIndex(abilityIndex);
            
            _screenView.AbilityDescription.Show(true);
            _screenView.UnlockButton.UnlockPrice.Price.text = slotModel.Price.ToString();
            _screenView.AbilityDescription.Icon.sprite = slotModel.Icon;
            _screenView.AbilityDescription.LocalizeString.StringReference = slotModel.Description;
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
                _audioService.PlaySound("Equip");
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
                _audioService.PlaySound("Equip");
                AbilitySelected(index);
            };

            UpdateData();
        }

        public virtual void Dispose()
        {
            _disposable.Dispose();
            
        }
    }
}
