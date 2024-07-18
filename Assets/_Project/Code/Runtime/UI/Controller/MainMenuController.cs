using System;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.Modules.LocalizationProvider;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.MainMenu;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class MainMenuController : IScreenController, IDisposable
    {
        protected MainMenuModel _model;
        protected MainMenuWindowView _windowView;
        
        private ScreenService _screenService;
        private readonly IGameDataContainer _gameData;
        private readonly LevelLoader _levelLoader;
        private readonly SceneLoader _sceneLoader;
        private readonly AudioService _audioService;
        private readonly LocalizationService _localService;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public MainMenuController(IGameDataContainer gameData, AudioService audioService, LevelLoader levelLoader, LocalizationService localService)
        {
            _gameData = gameData;
            _levelLoader = levelLoader;
            _audioService = audioService;
            _localService = localService;
        }
        
        public virtual void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as MainMenuModel;
            _windowView = windowView as MainMenuWindowView;
            _screenService = screenService;
            
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_windowView);

            _windowView.RussianLang
                .OnClickAsObservable()
                .Subscribe(x => _localService.ChangeLanguage(SystemLanguage.Russian));

            _windowView.EnglishLang
                .OnClickAsObservable()
                .Subscribe(x => _localService.ChangeLanguage(SystemLanguage.English));

            _windowView.TurkishLang
                .OnClickAsObservable()
                .Subscribe(x => _localService.ChangeLanguage(SystemLanguage.Turkish));
            
            InitializeButtons();
            InitializeStats();
            UpdateAllStatButtons();
        }


        private void InitializeStats()
        {
            InitializeStat(_model.Health, _windowView.StatWindow.Health, _model.CanUpgradeHealth, _model.UpgradeHealth);
            InitializeStat(_model.Attack, _windowView.StatWindow.Attack, _model.CanUpgradeAttack, _model.UpgradeAttack);
            InitializeStat(_model.Magic, _windowView.StatWindow.Magic, _model.CanUpgradeMagic, _model.UpgradeMagic);

            _model.Souls.Subscribe(_ => UpdateAllStatButtons()).AddTo(_disposables);

            _windowView.StatWindow.StatUpgradePrice.text = _model.StatUpgradePrice.ToString();
        }

        private void InitializeButtons()
        {
            _windowView.StartFightButton.OnClickAsObservable()
                .Subscribe(_ => _screenService.Open(ScreenID.ArenaSelectionScreen))
                .AddTo(_disposables);

            _windowView.StartFightButton.PlayScaleAnimation();

            _windowView.SkillScreen.OnClickAsObservable()
                .Subscribe(_ => _screenService.Open(_windowView.SkillScreen.WindowId))
                .AddTo(_disposables);

            _windowView.WeaponScreen.OnClickAsObservable()
                .Subscribe(_ => _screenService.Open(_windowView.WeaponScreen.WindowId))
                .AddTo(_disposables);
        }

        private void UpdateAllStatButtons()
        {
            _windowView.StatWindow.Health.ShowPlusButton(_model.CanUpgradeHealth());
            _windowView.StatWindow.Attack.ShowPlusButton(_model.CanUpgradeAttack());
            _windowView.StatWindow.Magic.ShowPlusButton(_model.CanUpgradeMagic());
        }


        private void InitializeStat(ReactiveProperty<int> stat, StatsUI statView, Func<bool> canUpgrade, Action upgrade)
        {
            stat.Subscribe(value =>
            {
                statView.SetStatValue(value);
                statView.ShowPlusButton(canUpgrade());
                UpdateAllStatButtons();
            }).AddTo(_disposables);

            statView.OnPlusClicked().Subscribe(_ => upgrade()).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
