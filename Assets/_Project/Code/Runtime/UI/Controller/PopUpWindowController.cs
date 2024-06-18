using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.DTO;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.MessageWindow;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class PopUpWindowController : IScreenController
    {
        private PopUpWindowModel _model;
        private MessageWindowView _windowView;
        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as PopUpWindowModel;
            _windowView = windowView as MessageWindowView;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_windowView);

            if (_model.ModelDto is MessageDto messageDto)
            {
                _windowView.Text.text = messageDto.Message;
            }

            else if (_model.ModelDto is TimerMessageDto updatableDto)
            {
                Observable.EveryUpdate().Subscribe(x =>
                {
                    var time = updatableDto.Seconds -= Time.deltaTime;
                    _windowView.Text.text = $"{updatableDto.Message} {Mathf.RoundToInt(updatableDto.Seconds)}";
                    if (time < 0)
                    {
                        screenService.Close(this);
                    }
                }).AddTo(_windowView);
            }
            

        }
    }
}
