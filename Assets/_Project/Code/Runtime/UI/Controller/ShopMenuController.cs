using Code.Runtime.Core.SceneManagement;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class ShopMenuController : IScreenController
    {
        private WeaponShopWindowModel _model;
        private ShopMenuWindowView _windowView;
        private readonly SceneLoader _sceneLoader;
        public ShopMenuController(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as WeaponShopWindowModel;
            _windowView = windowView as ShopMenuWindowView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_windowView);

            _windowView.NextSceneButton.OnClickAsObservable().Subscribe(x => _sceneLoader.Load("StoneDungeon_2"));
        }
    }
}
