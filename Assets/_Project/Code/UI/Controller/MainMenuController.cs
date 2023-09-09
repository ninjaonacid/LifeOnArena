using System;
using Code.Infrastructure.SceneManagement;
using Code.Services.PersistentProgress;
using Code.UI.Model;
using Code.UI.Services;
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
        private IScreenService _screenService;
        private readonly IGameDataContainer _gameData;
        private readonly SceneLoader _sceneLoader;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public MainMenuController(IGameDataContainer gameData, SceneLoader sceneLoader)
        {
            _gameData = gameData;
            _sceneLoader = sceneLoader;
        }
        
        public void InitController(IScreenModel model, BaseView view, IScreenService screenService)
        {
            _model = model as MainMenuModel;
            _view = view as MainMenuView;
            _screenService = screenService;

            _model.Health.Subscribe(x =>
            {
                _view.StatContainer.SetHealth(nameof(_model.Health) + " ", _model.Health.Value);
                _gameData.PlayerData.StatsData.Stats["Health"] = _model.Health.Value;
            }).AddTo(_disposables);
            
            _model.Attack.Subscribe(x =>
            {
                _view.StatContainer.SetAttack(nameof(_model.Attack) + " ", _model.Attack.Value);
                _gameData.PlayerData.StatsData.Stats["Attack"] = _model.Attack.Value;
            }).AddTo(_disposables);


            _model.Defense.Subscribe(x =>
            {
                _view.StatContainer.SetDefense(nameof(_model.Defense) + " ", _model.Defense.Value);
                _gameData.PlayerData.StatsData.Stats["Defense"] = _model.Defense.Value;
            }).AddTo(_disposables);

            _view.CloseButton
                .OnClickAsObservable()
                .Subscribe(x => Debug.Log("Button Pressed")).AddTo(_view);

            _view.StartFightButton
                .OnClickAsObservable()
                .Subscribe(x => _sceneLoader.Load("StoneDungeon_1"));

            _view.SkillMenu.Button
                .OnClickAsObservable()
                .Subscribe(x => _screenService.Open(_view.SkillMenu.WindowId));
        }
        

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
