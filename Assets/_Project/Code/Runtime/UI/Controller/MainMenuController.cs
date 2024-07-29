using System;
using System.Threading;
using Code.Runtime.Core.Audio;
using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Services.PauseService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.MainMenu;
using Cysharp.Threading.Tasks;
using InstantGamesBridge.Modules.Advertisement;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class MainMenuController : IScreenController, IDisposable
    {
        private MainMenuModel _model;
        protected MainMenuWindowView _windowView;

        private ScreenService _screenService;
        private readonly IGameDataContainer _gameData;
        private readonly AdvertisementService _adService;
        private readonly AudioService _audioService;
        private readonly SaveLoadService _saveLoad;
        private readonly PauseService _pauseService;

        private readonly CompositeDisposable _disposables = new();
        private readonly CancellationTokenSource _cts = new();

        public MainMenuController(IGameDataContainer gameData, AdvertisementService adService,
            AudioService audioService, SaveLoadService saveLoad, PauseService pauseService)
        {
            _gameData = gameData;
            _adService = adService;
            _audioService = audioService;
            _saveLoad = saveLoad;
            _pauseService = pauseService;
        }

        public virtual void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as MainMenuModel;
            _windowView = windowView as MainMenuWindowView;
            _screenService = screenService;

            Assert.IsNotNull(_model);
            Assert.IsNotNull(_windowView);

            InitializeButtons();
            InitializeStats();
            InitializeSouls();
            UpdateAllStatButtons();
        }

        private void InitializeSouls()
        {
            _windowView.ResourceCount.ChangeText(_model.Souls.Value.ToString());

            _model.Souls.Subscribe(x => { _windowView.ResourceCount.ChangeText(_model.Souls.Value.ToString()); });

            _model.Souls.Subscribe(_ => UpdateAllStatButtons()).AddTo(_disposables);
        }

        private void InitializeStats()
        {
            _windowView.StatWindow.Level.text = _model.Level.ToString();

            InitializeStat(_model.Health, _windowView.StatWindow.Health, _model.CanUpgradeHealth, _model.UpgradeHealth);
            InitializeStat(_model.Attack, _windowView.StatWindow.Attack, _model.CanUpgradeAttack, _model.UpgradeAttack);
            InitializeStat(_model.Magic, _windowView.StatWindow.Magic, _model.CanUpgradeMagic, _model.UpgradeMagic);

            _windowView.StatWindow.StatUpgradePrice.text = _model.StatUpgradePrice.ToString();
        }

        private void InitializeButtons()
        {
            _windowView.StartFightButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _audioService.PlaySound("ClickButton");
                    _screenService.Open(ScreenID.ArenaSelectionScreen);
                })
                .AddTo(_disposables);

            _windowView.StartFightButton.PlayScaleAnimation();

            _windowView.SkillScreen.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _audioService.PlaySound("ClickButton");
                    _screenService.Open(_windowView.SkillScreen.WindowId);
                })
                .AddTo(_disposables);

            _windowView.WeaponScreen.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _audioService.PlaySound("ClickButton");
                    _screenService.Open(_windowView.WeaponScreen.WindowId);
                })
                .AddTo(_disposables);

            _windowView.SettingsButton.OnClickAsObservable()
                .Subscribe(x =>
                {
                    _audioService.PlaySound("ClickButton");
                    _screenService.Open(ScreenID.MainMenuSettingsPopUpView);
                });

            int completedLocations = _gameData.PlayerData.WorldData.LocationProgressData.CompletedLocations.Count;
            int soulsRewards = _gameData.PlayerData.RewardsData.AdSoulsRewards;

            bool showReward = soulsRewards == 0 || completedLocations % soulsRewards == 0;


            if (showReward)
            {
                _adService.OnRewardedStateChange += HandleRewardedStateChange;
                _windowView.RewardButton.OnClickAsObservable()
                    .Subscribe(x => { _adService.ShowReward(); });

                _windowView.RewardButton.Show();
                _windowView.RewardButton.PlayScaleAnimation();
            }
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

            statView.OnPlusClicked().Subscribe(_ =>
            {
                _audioService.PlaySound("ClickButton");
                upgrade();
                _saveLoad.SaveData();
            }).AddTo(_disposables);
        }

        private void HandleRewardedStateChange(RewardedState state)
        {
            switch (state)
            {
                case RewardedState.Opened:
                    _pauseService.PauseGame();
                    _audioService.MuteSounds(true);
                    _audioService.MuteMusic(true);
                    break;

                case RewardedState.Rewarded:
                    HandleSoulsReward();

                    _windowView.RewardButton.Show(false);
                    break;

                case RewardedState.Closed:
                    _audioService.MuteMusic(!_gameData.AudioData.isMusicOn);
                    _audioService.MuteSounds(!_gameData.AudioData.isSoundOn);
                    _pauseService.UnpauseGame();
                    break;

                case RewardedState.Failed:
                    _audioService.MuteMusic(!_gameData.AudioData.isMusicOn);
                    _audioService.MuteSounds(!_gameData.AudioData.isSoundOn);
                    _pauseService.UnpauseGame();
                    break;
            }
        }

        private void HandleSoulsReward()
        {
            _gameData.PlayerData.WorldData.LootData.Collect(500);
            _gameData.PlayerData.RewardsData.AdSoulsRewards++;
            _saveLoad.SaveData();
        }


        public virtual void Dispose()
        {
            _disposables.Dispose();
            _cts.Cancel();
            _cts.Dispose();
            _adService.OnRewardedStateChange -= HandleRewardedStateChange;
        }
    }
}