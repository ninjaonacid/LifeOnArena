using System;
using System.Threading;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PersistentProgress;
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
        private readonly LevelLoader _levelLoader;
        private readonly SceneLoader _sceneLoader;
        private readonly AdvertisementService _adService;
        private readonly AudioService _audioService;

        private readonly CompositeDisposable _disposables = new();
        private readonly CancellationTokenSource _cts = new();

        public MainMenuController(IGameDataContainer gameData, AudioService audioService, LevelLoader levelLoader,
            AdvertisementService adService)
        {
            _gameData = gameData;
            _levelLoader = levelLoader;
            _audioService = audioService;
            _adService = adService;
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
        }

        private void InitializeStats()
        {
            _windowView.StatWindow.Level.text = _model.Level.ToString();

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

            _windowView.SettingsButton.OnClickAsObservable()
                .Subscribe(x => _screenService.Open(ScreenID.MainMenuSettingsPopUpView));

            int completedLocations = _gameData.PlayerData.WorldData.LocationProgressData.CompletedLocations.Count;

            if (completedLocations == 0)
            {
                return;
            }
            if(completedLocations % 2 == 0)
            {
                _windowView.RewardButton.OnClickAsObservable()
                    .Subscribe(x =>
                    {
                        _adService.ShowReward();
                        WaitForAdTask(_cts.Token).Forget();
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

        private async UniTask WaitForAdTask(CancellationToken token)
        {
            await UniTask.WaitUntil(() => _adService.RewardedState != RewardedState.Loading, cancellationToken: token);
            HandleAdResult(_adService.RewardedState);
        }

        private void HandleSoulsReward()
        {
            _gameData.PlayerData.WorldData.LootData.Collect(500);
            _screenService.Close(this);
        }

        private void HandleAdResult(RewardedState state)
        {
            switch (state)
            {
                case RewardedState.Rewarded:
                    HandleSoulsReward();
                    break;
                case RewardedState.Failed:
                    _levelLoader.LoadLevel("MainMenu");
                    break;
                case RewardedState.Closed:
                case RewardedState.Opened:
                    break;
                default:
                    Debug.LogWarning($"Unexpected RewardedState: {state}");
                    _screenService.Close(this);
                    break;
            }
        }

        public virtual void Dispose()
        {
            _disposables.Dispose();
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}