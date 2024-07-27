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

        public MainMenuController(IGameDataContainer gameData, AdvertisementService adService, AudioService audioService, SaveLoadService saveLoad, PauseService pauseService)
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
                .Subscribe(_ => _screenService.Open(ScreenID.ArenaSelectionScreen))
                .AddTo(_disposables);

            _windowView.StartFightButton.PlayScaleAnimation();

            _windowView.SkillScreen.OnClickAsObservable()
                .Subscribe(_ => _screenService.Open(_windowView.SkillScreen.WindowId))
                .AddTo(_disposables);

            _windowView.WeaponScreen.OnClickAsObservable()
                .Subscribe(_ => _screenService.Open(_windowView.WeaponScreen.WindowId))
                .AddTo(_disposables);

            _windowView.SettingsButton.OnClickAsObservable()
                .Subscribe(x => _screenService.Open(ScreenID.MainMenuSettingsPopUpView));

            int completedLocations = _gameData.PlayerData.WorldData.LocationProgressData.CompletedLocations.Count;

            if (completedLocations == 0)
            {
                return;
            }

            if (completedLocations % 2 == 1)
            {
                _windowView.RewardButton.OnClickAsObservable()
                    .Subscribe(x =>
                    {
                        ShowAdTask(_cts.Token).Forget();
                    });

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

            statView.OnPlusClicked().Subscribe(_ => upgrade()).AddTo(_disposables);
        }

        private async UniTask ShowAdTask(CancellationToken token)
        {
            _adService.ShowReward();
            RewardedState finalState;
            bool wasRewarded = false;
            do
            {
                await UniTask.WaitUntil(() => _adService.RewardedState != RewardedState.Loading,
                    cancellationToken: token);
                finalState = _adService.RewardedState;

                switch (finalState)
                {
                    case RewardedState.Opened:
                        _pauseService.PauseGame();
                        _audioService.MuteAll(true);
                        break;
                    
                    case RewardedState.Rewarded:
                        wasRewarded = true;
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
                
            } while (finalState != RewardedState.Closed | finalState != RewardedState.Failed);

            if (wasRewarded)
            {
                Debug.Log("SOUL REWARDED");
                HandleSoulsReward();
                _windowView.RewardButton.Show(false);
            }
        }

        private void HandleSoulsReward()
        {
            _gameData.PlayerData.WorldData.LootData.Collect(1000);
            Debug.Log("Player was rewarded");
            _saveLoad.SaveData();
        }


        public virtual void Dispose()
        {
            _disposables.Dispose();
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}