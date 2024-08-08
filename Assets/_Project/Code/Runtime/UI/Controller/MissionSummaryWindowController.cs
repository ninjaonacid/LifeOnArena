using Code.Runtime.Core.LevelLoaderService;
using Code.Runtime.Services;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.MissionSummaryWindowModel;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.MissionSummaryWindow;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class MissionSummaryWindowController : IScreenController
    {
        private MissionSummaryWindowModel _model;
        private MissionSummaryWindowView _view;

        private readonly LevelLoader _levelLoader;
        private readonly LevelCollectableTracker _collectableTracker;
        private readonly PlayerControls _playerControls;

        public MissionSummaryWindowController(LevelLoader levelLoader, 
            LevelCollectableTracker collectableTracker,
            PlayerControls playerControls)
        {
            _levelLoader = levelLoader;
            _collectableTracker = collectableTracker;
            _playerControls = playerControls;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _view = windowView as MissionSummaryWindowView;
            _model = model as MissionSummaryWindowModel;

            Assert.IsNotNull(_view);
            Assert.IsNotNull(_model);
            
            _playerControls.Player.Disable();
            
            _view.ConfirmButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    Debug.Log("ConfirmButtonPressed");
                    _playerControls.Enable();
                    _levelLoader.LoadLevel("MainMenu");
                });

            _view.EnemiesExp.SetText(_collectableTracker.GainedExperience.ToString("F0"));
            _view.LevelExp.SetText(_collectableTracker.ObjectiveExperienceReward.ToString("F0"));
            _view.TotalExp.SetText((_collectableTracker.GainedExperience + _collectableTracker.ObjectiveExperienceReward).ToString("F0"));
            
            _view.SoulsFromEnemies.SetText(_collectableTracker.SoulsLoot.ToString());
            _view.SoulsForLevel.SetText(_collectableTracker.ObjectiveSoulsReward.ToString());
            _view.TotalSouls.SetText((_collectableTracker.ObjectiveSoulsReward + _collectableTracker.SoulsLoot).ToString());
            
            _view.ConfirmButton.PlayScaleAnimation();
        }
    }
}