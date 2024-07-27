﻿using Code.Runtime.Core.Audio;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PauseService;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.HudSettingsPopupView;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class HudSettingsPopupController : IScreenController
    {
        private HudSettingsPopupView _view;
        private HudSettingsPopupModel _model;

        private readonly PauseService _pauseService;
        private readonly LevelLoader _levelLoader;
        private readonly AudioService _audioService;
        private readonly SaveLoadService _saveLoad;

        public HudSettingsPopupController(PauseService pauseService, LevelLoader levelLoader, AudioService audioService, SaveLoadService saveLoad)
        {
            _pauseService = pauseService;
            _levelLoader = levelLoader;
            _audioService = audioService;
            _saveLoad = saveLoad;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _view = windowView as HudSettingsPopupView;
            _model = model as HudSettingsPopupModel;
            
            Assert.IsNotNull(_view);
            Assert.IsNotNull(_model);
            
            SubscribeAudioButtons();
            
            SubscribeCloseButton(screenService);

            SubscribePortalButton();
        }

        private void SubscribePortalButton()
        {
            _view.ReturnToPortalButton.OnClickAsObservable().Subscribe(x =>
            {
                _pauseService.UnpauseGame();
                _saveLoad.SaveData();
                _levelLoader.LoadLevel("MainMenu");
            });
        }

        private void SubscribeCloseButton(ScreenService screenService)
        {
            _view.CloseButton.OnClickAsObservable()
                .Subscribe(x =>
                {
                    _pauseService.UnpauseGame();
                    screenService.Close(this);
                });
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
    }
}