using System;
using Code.Services.PersistentProgress;
using Code.UI.Model;
using Code.UI.View;
using Code.UI.View.MainMenu;
using UniRx;
using Debug = UnityEngine.Debug;

namespace Code.UI.Controller
{
    public class MainMenuController : IScreenController, IDisposable
    {
        private  MainMenuModel _model;
        private  MainMenuView _view;
        private readonly IGameDataContainer _gameData;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public MainMenuController(IGameDataContainer gameData)
        {
            _gameData = gameData;
        }
        
        public void InitController(IScreenModel model, BaseView view)
        {
            _model = model as MainMenuModel;
            _view = view as MainMenuView;

            _model.Attack.Value = _gameData.PlayerData.StatsData.Stats["Attack"];
            _model.Health.Value = _gameData.PlayerData.StatsData.Stats["Health"];

            _model.Attack.Subscribe(x =>
            {
                _view.StatContainer.SetAttack(nameof(_model.Attack), _model.Attack.Value);
                _gameData.PlayerData.StatsData.Stats["Attack"] = _model.Attack.Value;
            }).AddTo(_disposables);

            _model.Health.Subscribe(x =>
            {
                _view.StatContainer.SetHealth(nameof(_model.Health), _model.Health.Value);
                _gameData.PlayerData.StatsData.Stats["Health"] = _model.Health.Value;
            }).AddTo(_view);

            // _view.CloseButton
            //     .OnClickAsObservable()
            //     .Subscribe(x => Debug.Log("Button pressed"));
            
            _view.CloseButton.onClick.AddListener(() => Debug.Log("ButtonPressed"));
            
            Debug.Log("CloseButton", _view.CloseButton);

        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
