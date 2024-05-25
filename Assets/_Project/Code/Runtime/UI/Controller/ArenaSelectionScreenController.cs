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
        private ArenaSelectionScreenModel _model;
        private ArenaSelectionScreenView _view;
        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as ArenaSelectionScreenModel;
            _view = windowView as ArenaSelectionScreenView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_view);

            _view.CloseButton
                .OnClickAsObservable()
                .Subscribe(x => screenService.Close(this));;
            
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
    }
}
