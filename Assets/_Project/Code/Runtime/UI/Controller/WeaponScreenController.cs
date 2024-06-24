using Code.Runtime.Core.SceneManagement;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.WeaponShopView;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class WeaponScreenController : IScreenController
    {
        private WeaponScreenModel _model;
        private WeaponScreenView _windowView;
        private readonly SceneLoader _sceneLoader;
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
                .Subscribe( x => screenService.Close(_windowView.ScreenId));
        }
    }
}
