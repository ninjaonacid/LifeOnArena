using Code.Services.PersistentProgress;
using Code.UI.Model;
using Code.UI.View;
using UniRx;

namespace Code.UI.Controller
{
    public class MainMenuController : IScreenController
    {
        private MainMenuModel _model;
        private MainMenuView _view;
        private GameDataService _gameData;
        
        public MainMenuController(MainMenuModel model, MainMenuView view, GameDataService gameData)
        {
            _model = model;
            _view = view;
            _gameData = gameData;
        }


        public void InitController()
        {
           _model.Attack.Subscribe( x=> _view)
        }
    }
}
