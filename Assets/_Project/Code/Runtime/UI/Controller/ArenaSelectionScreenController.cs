using System;
using Code.Runtime.Core.Audio;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.ArenaSelectionScreenModel;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.ArenaSelection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class ArenaSelectionScreenController : IScreenController, IDisposable
    {
        protected ArenaSelectionScreenModel _model;
        protected ArenaSelectionScreenView _view;
        private readonly LevelLoader _levelLoader;
        private readonly SaveLoadService _saveLoad;
        private readonly AudioService _audioService;
        
        protected readonly CompositeDisposable _disposable = new();

        public ArenaSelectionScreenController(LevelLoader levelLoader, SaveLoadService saveLoad, AudioService audioService)
        {
            _levelLoader = levelLoader;
            _saveLoad = saveLoad;
            _audioService = audioService;
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
                .Subscribe(x => screenService.Close(this))
                .AddTo(_disposable);

            _view.LevelContainer
                .OnLevelSelectedAsObservable()
                .Subscribe(LevelSelected)
                .AddTo(_disposable);
            
            _view.StartBattleButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    _audioService.PlaySound("ClickButton");
                    StartBattle();
                })
                .AddTo(_disposable);
            
            UpdateData();

            _view.LevelContainer.SelectCurrentLevel();

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
                if (locationId != -1)
                {
                    _saveLoad.SaveData();
                    _levelLoader.LoadLevel(locationId);
                }
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

        public virtual void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
