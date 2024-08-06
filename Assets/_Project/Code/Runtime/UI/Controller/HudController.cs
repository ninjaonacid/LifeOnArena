using System;
using Code.Runtime.Core.EventSystem;
using Code.Runtime.Core.Factory;
using Code.Runtime.CustomEvents;
using Code.Runtime.Entity;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Modules.LocalizationProvider;
using Code.Runtime.Modules.WebApplicationModule;
using Code.Runtime.Services;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PauseService;
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
        private readonly PauseService _pauseService;
        private readonly LevelCollectableTracker _collectableTracker;
        private readonly LocalizationService _localService;
        private ScreenService _screenService;

        private readonly CompositeDisposable _disposable = new();

        public HudController(
            IGameDataContainer gameData, 
            HeroFactory heroFactory, 
            LevelLoader sceneLoader,
            IEventSystem eventSystem, 
            AdvertisementService adService,
            PauseService pauseService,
            LevelCollectableTracker collectableTracker,
            LocalizationService localService)
        {
            _gameData = gameData;
            _heroFactory = heroFactory;
            _levelLoader = sceneLoader;
            _eventSystem = eventSystem;
            _adService = adService;
            _collectableTracker = collectableTracker;
            _pauseService = pauseService;
            _localService = localService;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as HudModel;
            _windowView = windowView as HudWindowView;
            _screenService = screenService;

            Assert.IsNotNull(_windowView);
            Assert.IsNotNull(_model);

            LinkHeroComponents();
            SubscribeStatusBar();
            SubscribeButtons();

            _eventSystem.Subscribe<BossSpawnEvent>(SubscribeBossHealthBar);
            _eventSystem.Subscribe<BossKillEvent>(HandleBossKilledLogic);
            _eventSystem.Subscribe<LevelEndEvent>(ShowReturnToMenuButton);
            
        }

        private void HandleBossKilledLogic(BossKillEvent bossKillEvent)
        {
            _windowView.SettingsButton.Show(false);
            _windowView.BossHudHealthBar.Show(false);
        }

        private void SubscribeButtons()
        {
            _windowView.PortalButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    _screenService.Open(ScreenID.MissionSummaryWindow,
                        new MissionSummaryDto(
                            _collectableTracker.GainedExperience,
                            _collectableTracker.KilledEnemies,
                            _collectableTracker.SoulsLoot,
                            _collectableTracker.ObjectiveExperienceReward));
                });

            _windowView.SettingsButton
                .OnClickAsObservable()
                .Subscribe(x =>
                {
                    _pauseService.PauseGame();
                    _screenService.Open(ScreenID.HudSettingsPopup);
                });

            if (WebApplication.IsMobile)
            {
                _windowView.ControlsButton.Show(false);
                _windowView.Joystick.Show(true);
            }
            else
            {
                _windowView.ControlsButton.Show(true);
                
                _windowView.ControlsButton
                    .OnClickAsObservable()
                    .Subscribe(x =>
                    {
                        _screenService.Open(ScreenID.HudControlsScreen);
                    });
            }
        }

        private void LinkHeroComponents()
        {
            _heroFactory.HeroGameObject.TryGetComponent(out HeroAttackComponent heroAttack);
            _heroFactory.HeroGameObject.TryGetComponent(out HeroAbilityController heroSkills);
            _heroFactory.HeroGameObject.TryGetComponent(out HeroHealth heroHealth);
            _heroFactory.HeroGameObject.TryGetComponent(out AbilityCooldownController heroCooldown);

            _windowView.ComboCounter.Construct(heroAttack, heroHealth, _localService);
            _windowView.LootCounter.Construct(_gameData.PlayerData.WorldData);
            _windowView.HudSkillContainer.Construct(heroSkills, heroCooldown);
            _windowView.GetComponent<EntityUI>().Construct(heroHealth);
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
            _windowView.BossHudHealthBar.SetBossName(obj.BossGo.GetComponent<Actor>().ActorName.GetLocalizedString());

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
            _windowView.PortalButton.AnimateArrow();
        }
        
        
        public void Dispose()
        {
            _disposable.Dispose();
            _eventSystem.Unsubscribe<BossSpawnEvent>(SubscribeBossHealthBar);
            _eventSystem.Unsubscribe<LevelEndEvent>(ShowReturnToMenuButton);
            _eventSystem.Unsubscribe<BossKillEvent>(HandleBossKilledLogic);
        }
    }
}