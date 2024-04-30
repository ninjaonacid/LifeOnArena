using Code.Runtime.Core.Factory;
using Code.Runtime.Core.SceneManagement;
using Code.Runtime.Entity.Hero;
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
    public class HudController : IScreenController
    {
        private HudModel _model;
        private HudWindowView _windowView;

        private readonly IGameDataContainer _gameData;
        private readonly IHeroFactory _heroFactory;
        private readonly SceneLoader _sceneLoader;
        
        public HudController(IGameDataContainer gameData, IHeroFactory heroFactory, SceneLoader sceneLoader)
        {
            _gameData = gameData;
            _heroFactory = heroFactory;
            _sceneLoader = sceneLoader;
        }

        public void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService)
        {
            _model = model as HudModel;
            _windowView = windowView as HudWindowView;

            Assert.IsNotNull(_windowView);
            Assert.IsNotNull(_model);

            _heroFactory.HeroGameObject.TryGetComponent(out HeroAttackComponent heroAttack);
            _heroFactory.HeroGameObject.TryGetComponent(out HeroSkills heroSkills);
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
                    _gameData.PlayerData.PlayerExp.ExperienceToNextLevel));

            _gameData.PlayerData.PlayerExp
                .OnLevelChangedAsObservable()
                .Subscribe(x => _windowView.StatusBar.SetLevel(_gameData.PlayerData.PlayerExp.Level));
            
            _windowView.StatusBar.SetExpValue(_gameData.PlayerData.PlayerExp.Experience, _gameData.PlayerData.PlayerExp.ExperienceToNextLevel);
            _windowView.StatusBar.SetLevel(_gameData.PlayerData.PlayerExp.Level);
        

            _windowView.RestartButton.onClick.AsObservable().Subscribe(x => _sceneLoader.Load("MainMenu"));



        }
    }
}
