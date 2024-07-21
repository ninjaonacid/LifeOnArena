using System;
using Code.Runtime.Core.EventSystem;
using Code.Runtime.Core.Factory;
using Code.Runtime.CustomEvents;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Services;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Model.DTO;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.HUD;
using UniRx;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class HudController : IScreenController, IDisposable
    {
        private HudModel _model;
        private HudWindowView _windowView;

        private readonly IGameDataContainer _gameData;
        private readonly HeroFactory _heroFactory;
        private readonly LevelLoader _levelLoader;
        private readonly IEventSystem _eventSystem;
        private readonly AdvertisementService _adService;
        private readonly LevelCollectableTracker _collectableTracker;
        private ScreenService _screenService;

        private readonly CompositeDisposable _disposable = new();

        public HudController(IGameDataContainer gameData, 
            HeroFactory heroFactory, LevelLoader sceneLoader,
            IEventSystem eventSystem, AdvertisementService adService,
            LevelCollectableTracker collectableTracker)
        {
            _gameData = gameData;
            _heroFactory = heroFactory;
            _levelLoader = sceneLoader;
            _eventSystem = eventSystem;
            _adService = adService;
            _collectableTracker = collectableTracker;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as HudModel;
            _windowView = windowView as HudWindowView;
            _screenService = screenService;

            Assert.IsNotNull(_windowView);
            Assert.IsNotNull(_model);

            _heroFactory.HeroGameObject.TryGetComponent(out HeroAttackComponent heroAttack);
            _heroFactory.HeroGameObject.TryGetComponent(out HeroAbilityController heroSkills);
            _heroFactory.HeroGameObject.TryGetComponent(out HeroHealth heroHealth);
            _heroFactory.HeroGameObject.TryGetComponent(out AbilityCooldownController heroCooldown);

            _windowView.ComboCounter.Construct(heroAttack, heroHealth);
            _windowView.LootCounter.Construct(_gameData.PlayerData.WorldData);
            _windowView.HudSkillContainer.Construct(heroSkills, heroCooldown);
            _windowView.GetComponent<EntityUI>().Construct(heroHealth);
            
            SubscribeStatusBar();
            _windowView.PortalButton.OnClickAsObservable().Subscribe(x =>
            {
                _screenService.Open(ScreenID.MissionSummaryWindow, 
                    new MissionSummaryDto(
                        _collectableTracker.GainedExperience, 
                        _collectableTracker.KilledEnemies, 
                        _collectableTracker.SoulsLoot,
                        _collectableTracker.ObjectiveExperienceReward)); 
            });

            _windowView.RestartButton.onClick.AsObservable().Subscribe(x => _levelLoader.LoadLevel("MainMenu"))
                .AddTo(_disposable);

            _windowView.RewardButton.onClick.AsObservable().Subscribe(x => _adService.ShowReward()).AddTo(_disposable);

            _eventSystem.Subscribe<BossSpawnEvent>(SubscribeBossHealthBar);
            _eventSystem.Subscribe<LevelEndEvent>(ShowReturnToMenuButton);
        }

        private void SubscribeStatusBar()
        {
            var playerExp = _gameData.PlayerData.PlayerExp;
            _windowView.StatusBar.SetExpValue(_gameData.PlayerData.PlayerExp.ProgressToNextLevel);
            _windowView.StatusBar.SetLevel(_gameData.PlayerData.PlayerExp.Level);

            playerExp.OnExperienceChangedAsObservable()
                .Subscribe(x =>
                    _windowView.StatusBar
                        .SetExpValue(_gameData.PlayerData.PlayerExp.ProgressToNextLevel))
                .AddTo(_disposable);

            playerExp.OnLevelChangedAsObservable()
                .Subscribe(x => _windowView.StatusBar.SetLevel(_gameData.PlayerData.PlayerExp.Level))
                .AddTo(_disposable);
        }

        private void SubscribeBossHealthBar(BossSpawnEvent obj)
        {
            var damageable = obj.BossGo.GetComponent<IDamageable>();

            _windowView.BossHudHealthBar.Show(true);
            _windowView.BossHudHealthBar.SetBossName(obj.BossId.name);

            Observable.FromEvent(x => damageable.Health.CurrentValueChanged += x,
                    x => damageable.Health.CurrentValueChanged -= x)
                .Subscribe(x =>
                    _windowView.BossHudHealthBar.UpdateHpBar(damageable.Health.Value, damageable.Health.CurrentValue))
                .AddTo(_disposable);
        }

        private void ShowReturnToMenuButton(LevelEndEvent obj)
        {
            _windowView.PortalButton.Show(true);
            _windowView.PortalButton.PlayScaleAnimation();
        }
        
        public void Dispose()
        {
            _disposable.Dispose();
            _eventSystem.Unsubscribe<BossSpawnEvent>(SubscribeBossHealthBar);
        }
    }
}