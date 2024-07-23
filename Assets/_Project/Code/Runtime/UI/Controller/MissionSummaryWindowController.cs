using Code.Runtime.Services;
using Code.Runtime.Services.LevelLoaderService;
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

        public MissionSummaryWindowController(LevelLoader levelLoader, LevelCollectableTracker collectableTracker)
        {
            _levelLoader = levelLoader;
            _collectableTracker = collectableTracker;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _view = windowView as MissionSummaryWindowView;
            _model = model as MissionSummaryWindowModel;

            Assert.IsNotNull(_view);
            Assert.IsNotNull(_model);
            
            _view.ConfirmButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    Debug.Log("ConfirmButtonPressed");
                    _levelLoader.LoadLevel("MainMenu");
                });

            _view.EnemiesExp.SetText(_collectableTracker.GainedExperience.ToString());
            _view.LevelExp.SetText(_collectableTracker.ObjectiveExperienceReward.ToString());
            _view.TotalExp.SetText((_collectableTracker.GainedExperience + _collectableTracker.ObjectiveExperienceReward).ToString());
            
            _view.SoulsFromEnemies.SetText(_collectableTracker.SoulsLoot.ToString());
            _view.SoulsForLevel.SetText(_collectableTracker.ObjectiveSoulsReward.ToString());
            _view.TotalSouls.SetText((_collectableTracker.ObjectiveSoulsReward + _collectableTracker.SoulsLoot).ToString());
            
            _view.ConfirmButton.PlayScaleAnimation();
        }
    }
}