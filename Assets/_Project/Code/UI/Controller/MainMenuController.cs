using Code.Services;
using Code.Services.PersistentProgress;
using Code.UI.Model;
using Code.UI.View;
using UniRx;

namespace Code.UI.Controller
{
    public class MainMenuController : IScreenController
    {
        private readonly MainMenuModel _model;
        private readonly MainMenuView _view;
        private readonly GameDataService _gameData;
        private IConfigDataProvider _configData;
        
        public MainMenuController(MainMenuModel model, MainMenuView view, GameDataService gameData)
        {
            _model = model;
            _view = view;
            _gameData = gameData;
        }
        
        public void InitController()
        {
            _model.Attack.Value = _gameData.PlayerData.StatsData.Stats["Attack"];
            _model.Health.Value = _gameData.PlayerData.StatsData.Stats["Health"];
            
            _model.Attack.Subscribe(x =>
            {
                _view.StatContainer.SetAttack(nameof(_model.Attack), _model.Attack.Value);
                _gameData.PlayerData.StatsData.Stats["Attack"] = _model.Attack.Value; 
            });

            _model.Health.Subscribe(x =>
            {
                _gameData.PlayerData.StatsData.Stats["Health"] = _model.Health.Value;
            });

        }
    }
}
