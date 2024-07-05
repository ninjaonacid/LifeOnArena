using System;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.WeaponScreen;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.WeaponShopView;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class WeaponScreenController : IScreenController, IDisposable
    {
        private WeaponScreenModel _model;
        private WeaponScreenView _windowView;
        private readonly CompositeDisposable _disposables = new();

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as WeaponScreenModel;
            _windowView = windowView as WeaponScreenView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_windowView);

            _windowView.CloseButton
                .OnClickAsObservable()
                .Subscribe( x => screenService.Close(this)).AddTo(_disposables);
            
            _windowView.WeaponContainer.Initialize();
            _windowView.WeaponContainer.OnWeaponCellSelectedAsObservable().Subscribe(WeaponSelected);
            _windowView.EquipButton.OnClickAsObservable().Subscribe(x => EquipWeapon());
            UpdateView();
        }

        private void UpdateView()
        {
            var weaponList = _model.GetSlots();
            
            for (var index = 0; index < weaponList.Count; index++)
            {
                var weapon = weaponList[index];
                
                _windowView.WeaponContainer.UpdateView(weapon.WeaponId, weapon.WeaponName.GetLocalizedString(), weapon.WeaponDescription.GetLocalizedString(), weapon.WeaponIcon, weapon.isUnlocked);
            }
        }
        
        private void WeaponSelected(int index)
        {
            if (_model.IsEquipped(index))
            {
                
            }
        }

        private void EquipWeapon()
        {
            int index;
            
            if (_windowView.WeaponContainer.TryGetSelectedWeaponId(out index))
            {
                _model.EquipWeapon(index);
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}