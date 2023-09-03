using Code.Infrastructure.SceneManagement;
using Code.UI.Model;
using Code.UI.View;
using UniRx;

namespace Code.UI.Controller
{
    public class ShopMenuController : IScreenController
    {

        private ShopMenuModel _model;
        private ShopMenuView _view;
        private SceneLoader _sceneLoader;
        public ShopMenuController(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        public void InitController(IScreenModel model, BaseView view)
        {
            _model = model as ShopMenuModel;
            _view = view as ShopMenuView;

            _view.NextSceneButton.OnClickAsObservable().Subscribe(x => _sceneLoader.Load("StoneDungeon_2"));
        }
    }
}
