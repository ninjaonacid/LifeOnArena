using Code.Services;
using Code.Services.PersistentProgress;
using Code.UI.Model;
using Code.UI.View;
using UniRx;

namespace Code.UI.Controller
{
    public class MainMenuController : IScreenController<MainMenuModel, MainMenuView>
    {
        private  MainMenuModel _model;
        private  MainMenuView _view;
        private readonly IGameDataContainer _gameData;
        private IConfigDataProvider _configData;
        
        public MainMenuController(IGameDataContainer gameData)
        {
            _gameData = gameData;
        }
        
        public void InitController(MainMenuModel model, MainMenuView view)
        {
            _model = model;
            _view = view;

            _model.Attack.Value = _gameData.PlayerData.StatsData.Stats["Attack"];
            _model.Health.Value = _gameData.PlayerData.StatsData.Stats["Health"];

            _model.Attack.Subscribe(x =>
            {
                _view.StatContainer.SetAttack(nameof(_model.Attack), _model.Attack.Value);
                _gameData.PlayerData.StatsData.Stats["Attack"] = _model.Attack.Value; 
            });

            _model.Health.Subscribe(x =>
            {
                _view.StatContainer.SetHealth(nameof(_model.Health), _model.Health.Value);
                _gameData.PlayerData.StatsData.Stats["Health"] = _model.Health.Value;
            });

        }

    }
}
