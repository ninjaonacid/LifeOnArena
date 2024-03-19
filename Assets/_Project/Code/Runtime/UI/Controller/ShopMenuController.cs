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
        private WeaponShopMenuModel _model;
        private ShopMenuView _view;
        private readonly SceneLoader _sceneLoader;
        public ShopMenuController(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void InitController(IScreenModel model, BaseView view, ScreenService screenService)
        {
            _model = model as WeaponShopMenuModel;
            _view = view as ShopMenuView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);

            _view.NextSceneButton.OnClickAsObservable().Subscribe(x => _sceneLoader.Load("StoneDungeon_2"));
        }
    }
}
