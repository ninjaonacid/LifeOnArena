using System;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.UI.Model;
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
        private readonly SceneLoader _sceneLoader;
        private readonly CompositeDisposable _disposables = new();
        public WeaponScreenController(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as WeaponScreenModel;
            _windowView = windowView as WeaponScreenView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_windowView);

            _windowView.CloseButton
                .OnClickAsObservable()
                .Subscribe( x => screenService.Close(this)).AddTo(_disposables);
            
            
        }


        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
