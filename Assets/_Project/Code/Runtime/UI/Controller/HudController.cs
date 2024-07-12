using System;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.EventSystem;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.CustomEvents;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Logic.WaveLogic;
using Code.Runtime.Modules.Advertisement;
using Code.Runtime.Services.LevelLoaderService;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.HUD;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
{
    public class HudController : IScreenController, IDisposable
    {
        private HudModel _model;
        private HudWindowView _windowView;

        private readonly IGameDataContainer _gameData;
        private readonly IHeroFactory _heroFactory;
        private readonly LevelLoader _levelLoader;
        private readonly IEventSystem _eventSystem;
        private readonly AdvertisementService _adService;
        
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        
        public HudController(IGameDataContainer gameData, IHeroFactory heroFactory, LevelLoader sceneLoader, 
            IEventSystem eventSystem, AdvertisementService adService)
        {
            _gameData = gameData;
            _heroFactory = heroFactory;
            _levelLoader = sceneLoader;
            _eventSystem = eventSystem;
            _adService = adService;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as HudModel;
            _windowView = windowView as HudWindowView;

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

            _gameData.PlayerData.PlayerExp
                .OnExperienceChangedAsObservable()
                .Subscribe( x =>
                _windowView.StatusBar.SetExpValue(_gameData.PlayerData.PlayerExp.Experience,
                    _gameData.PlayerData.PlayerExp.ExperienceToNextLevel)).AddTo(_disposable);

            _gameData.PlayerData.PlayerExp
                .OnLevelChangedAsObservable()
                .Subscribe(x => _windowView.StatusBar.SetLevel(_gameData.PlayerData.PlayerExp.Level)).AddTo(_disposable);
            
            _windowView.StatusBar.SetExpValue(_gameData.PlayerData.PlayerExp.Experience, _gameData.PlayerData.PlayerExp.ExperienceToNextLevel);
            _windowView.StatusBar.SetLevel(_gameData.PlayerData.PlayerExp.Level);
        

            _windowView.RestartButton.onClick.AsObservable().Subscribe(x => _levelLoader.LoadLevel("MainMenu"));

            _windowView.RewardButton.onClick.AsObservable().Subscribe(x => _adService.ShowReward());

            _eventSystem.Subscribe<BossSpawnEvent>(SubscribeHealthBar);

        }

        private void SubscribeHealthBar(BossSpawnEvent obj)
        {
            var damageable = obj.BossGo.GetComponent<IDamageable>();
            
            _windowView.BossHudHealthBar.Show(true);

            Observable.FromEvent(x => damageable.Health.CurrentValueChanged += x,
                    x => damageable.Health.CurrentValueChanged -= x)
                .Subscribe(x =>
                    _windowView.BossHudHealthBar.UpdateHpBar(damageable.Health.Value, damageable.Health.CurrentValue))
                .AddTo(_disposable);
            
            _windowView.BossHudHealthBar.SetBossName(obj.BossId.name);
        }

        private void SubscribeHealthBar(GameObject go, MobIdentifier mobId)
        {
            
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _eventSystem.Unsubscribe<BossSpawnEvent>(SubscribeHealthBar);
        }
    }
}
