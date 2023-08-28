using Code.Services;
using Code.Services.PersistentProgress;
using Code.UI.Model;
using Code.UI.View;
using UniRx;
using UnityEngine;

namespace Code.UI.Controller
{
    public class MainMenuController : IScreenController
    {
        private MainMenuModel _model;
        private MainMenuView _view;
        private GameDataService _gameData;
        private IStaticDataService _staticData;
        
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
