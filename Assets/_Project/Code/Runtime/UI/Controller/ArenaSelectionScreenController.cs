using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.ArenaSelectionScreenModel;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.ArenaSelection;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class ArenaSelectionScreenController : IScreenController
    {
        protected ArenaSelectionScreenModel _model;
        protected ArenaSelectionScreenView _view;
        private LevelLoader _levelLoader;

        public ArenaSelectionScreenController(LevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }

        public virtual void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as ArenaSelectionScreenModel;
            _view = windowView as ArenaSelectionScreenView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);
            
            _view.LevelContainer.Initialize();

            _view.CloseButton
                .OnClickAsObservable()
                .Subscribe(x => screenService.Close(this));;

            _view.LevelContainer.OnLevelSelectedAsObservable().Subscribe(LevelSelected);
            _view.StartBattleButton.OnClickAsObservable().Subscribe(x => StartBattle());
            
            UpdateData();

        }


        private void UpdateData()
        {
            for (var index = 0; index < _model.LocationPointModel.Count; index++)
            {
                var levelModel = _model.LocationPointModel[index];
                _view.LevelContainer.UpdateData(levelModel.LevelId, levelModel.LocationName, levelModel.Icon, levelModel.IsUnlocked, levelModel.IsCompleted);
            }
        }

        private void StartBattle()
        {
            var locationId =  _view.LevelContainer.GetSelectedLocationId();
            if (locationId == -1)
            {
                return;
            }
            var levelModel = _model.GetLevelModel(locationId);
            
            if (levelModel.IsUnlocked)
            {
                if(locationId != -1)
                    _levelLoader.LoadLevel(locationId);
            }
        }
        
        private void LevelSelected(int levelId)
        {
            var levelModel = _model.GetLevelModel(levelId);

            if (levelModel.IsUnlocked)
            {
                _view.StartBattleButton.BattleText.Show();
                _view.StartBattleButton.LockedText.Hide();
                _view.LockedInfo.Hide();
                _view.LocationInfo.Show();
                _view.StartBattleButton.PlayScaleAnimation();
                _view.LocationInfo.DifficultyInfo.SetDifficultyInfo(levelModel.Difficulty);
                _view.LocationInfo.Area.text = levelModel.LocationName;
                _view.LocationInfo.ObjectiveText.text = levelModel.LevelObjective;
            }
            else if(!levelModel.IsUnlocked)
            {
                _view.StartBattleButton.BattleText.Hide();
                _view.StartBattleButton.LockedText.Show();
                _view.LocationInfo.Hide();
                _view.LockedInfo.Show();
                _view.StartBattleButton.StopAnimation();
            }
            
            //UpdateData();
        }
    }
}
