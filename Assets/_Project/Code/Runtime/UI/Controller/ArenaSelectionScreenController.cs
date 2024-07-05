using Code.Runtime.Services.LevelLoader;
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
                _view.LevelContainer.UpdateData(index, levelModel.LocationName, levelModel.Icon, true);
            }
        }

        private void StartBattle()
        {
            var locationId =  _view.LevelContainer.GetSelectedLocationId();
           
           if(locationId != -1)
               _levelLoader.LoadLevel(locationId);
        }
        
        private void LevelSelected(int levelId)
        {
            var isUnlocked = _model.IsLevelUnlocked(levelId);

            if (isUnlocked)
            {
                _view.StartBattleButton.PlayScaleAnimation();
            }
            else
            {
                _view.StartBattleButton.StopAnimation();
            }
            
            //UpdateData();
        }
    }
}
