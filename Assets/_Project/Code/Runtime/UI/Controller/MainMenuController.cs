using System;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.MainMenu;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class MainMenuController : IScreenController, IDisposable
    {
        private  MainMenuModel _model;
        private  MainMenuWindowView _windowView;
        
        private ScreenService _screenService;
        private readonly IGameDataContainer _gameData;
        private readonly SceneLoader _sceneLoader;
        private readonly AudioService _audioService; 
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public MainMenuController(IGameDataContainer gameData, AudioService audioService, SceneLoader sceneLoader)
        {
            _gameData = gameData;
            _sceneLoader = sceneLoader;
            _audioService = audioService;
        }
        
        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as MainMenuModel;
            _windowView = windowView as MainMenuWindowView;
            _screenService = screenService;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_windowView);
            
            if (_model.IsTutorialEnabled)
            {
                _windowView.StartFightButton.interactable = false;
            }

            _model.Health.Subscribe(x =>
            {
                _windowView.StatContainer.SetHealth(nameof(_model.Health) + " ", _model.Health.Value);
                _gameData.PlayerData.StatsData.Stats["Health"] = _model.Health.Value;
            }).AddTo(_disposables);
            
            _model.Attack.Subscribe(x =>
            {
                _windowView.StatContainer.SetAttack(nameof(_model.Attack) + " ", _model.Attack.Value);
                _gameData.PlayerData.StatsData.Stats["Attack"] = _model.Attack.Value;
            }).AddTo(_disposables);


            _model.Defense.Subscribe(x =>
            {
                _windowView.StatContainer.SetDefense(nameof(_model.Defense) + " ", _model.Defense.Value);
                _gameData.PlayerData.StatsData.Stats["Defense"] = _model.Defense.Value;
            }).AddTo(_disposables);

      
            // _windowView.CloseButton
            //     .OnClickAsObservable()
            //     .Subscribe(x => _screenService.Close(this));

            _windowView.StartFightButton
                .OnClickAsObservable()
                .Subscribe(x => //_screenService.Open(ScreenID.ArenaSelectionScreen)
                    _sceneLoader.Load("FantasyArena_1")
                );

            _windowView.SkillScreen.Button
                .OnClickAsObservable()
                .Subscribe(x => _screenService.Open(_windowView.SkillScreen.WindowId));

            
            

        }
        

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
