﻿using Code.Runtime.UI.Model;
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
            
            UpdateData();

        }


        private void UpdateData()
        {
            for (var index = 0; index < _model.LevelModel.Count; index++)
            {
                var levelModel = _model.LevelModel[index];
                _view.LevelContainer.UpdateData(index, levelModel.LevelConfig.Icon, true);
            }
        }
        
        private void LevelSelected(int abilityIndex)
        {
            var isUnlocked = _model.IsLevelUnlocked(abilityIndex);
            
            // _view.EquipButton.ShowButton(!isEquipped && isUnlocked);
            // _view.UnEquipButton.ShowButton(isEquipped && isUnlocked);
            // _view.UnlockButton.ShowButton(!isEquipped && !isUnlocked);
            
            UpdateData();
            

        }
    }
}
