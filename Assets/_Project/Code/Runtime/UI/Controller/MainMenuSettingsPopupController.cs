using Code.Runtime.Core.Audio;
using Code.Runtime.Modules.LocalizationProvider;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.MainMenuSettingsPopupView;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class MainMenuSettingsPopupController : IScreenController
    {
        private MainMenuSettingsPopupView _view;
        private MainMenuSettingsPopupModel _model;

        private readonly LocalizationService _localService;
        private readonly AudioService _audioService;
        private ScreenService _screenService;

        public MainMenuSettingsPopupController(LocalizationService localizationService, AudioService audioService)
        {
            _localService = localizationService;
            _audioService = audioService;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _view = windowView as MainMenuSettingsPopupView;
            _model = model as MainMenuSettingsPopupModel;
            _screenService = screenService;

            Assert.IsNotNull(_view);
            Assert.IsNotNull(_model);

            SubscribeLanguageButtons();
            SubscribeAudioButtons();
            SubscribeCloseButton();
        }

        private void SubscribeLanguageButtons()
        {
            _view.EnglishLanguage
                .OnClickAsObservable()
                .Subscribe(x => _localService.ChangeLanguage(SystemLanguage.English));

            _view.RussianLanguage.OnClickAsObservable()
                .Subscribe(x => _localService.ChangeLanguage(SystemLanguage.Russian));

            _view.TurkishLanguage.OnClickAsObservable()
                .Subscribe(x => _localService.ChangeLanguage(SystemLanguage.Turkish));
        }

        private void SubscribeAudioButtons()
        {
            _view.MusicButton.SetButton(_model.IsMusicOn);
            _view.SoundButton.SetButton(_model.IsSoundOn);

            _view.MusicButton.OnClickAsObservable()
                .Subscribe(x =>
                {
                    _model.ChangeMusicState(!_model.IsMusicOn);
                    _audioService.MuteMusic(!_model.IsMusicOn);
                    _view.MusicButton.SetButton(_model.IsMusicOn);
                });

            _view.SoundButton.OnClickAsObservable()
                .Subscribe(x =>
                {
                    _model.ChangeSoundState(!_model.IsSoundOn);
                    _audioService.MuteSounds(!_model.IsSoundOn);
                    _view.SoundButton.SetButton(_model.IsSoundOn);
                });
        }

        private void SubscribeCloseButton()
        {
            _view.CloseButton.OnClickAsObservable()
                .Subscribe(x => { _screenService.Close(this); });
        }
    }
}