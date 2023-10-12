using Code.Core.Factory;
using Code.Entity.Hero;
using Code.Services.PersistentProgress;
using Code.UI.Model;
using Code.UI.Services;
using Code.UI.View;
using Code.UI.View.HUD;
using UnityEngine.Assertions;

namespace Code.UI.Controller
{
    public class HudController : IScreenController
    {
        private HudModel _model;
        private HudView _view;

        private readonly IGameDataContainer _gameData;
        private readonly IHeroFactory _heroFactory;
        
        public HudController(IGameDataContainer gameData, IHeroFactory heroFactory)
        {
            _gameData = gameData;
            _heroFactory = heroFactory;
        }

        public void InitController(IScreenModel model, BaseView view, IScreenService screenService)
        {
            _model = model as HudModel;
            _view = view as HudView;

            Assert.IsNotNull(_view);
            Assert.IsNotNull(_model);

            if (_heroFactory.HeroGameObject.TryGetComponent(out HeroAttack heroAttack));
            if (_heroFactory.HeroGameObject.TryGetComponent(out HeroSkills heroSkills));
            if (_heroFactory.HeroGameObject.TryGetComponent(out HeroHealth heroHealth));
            
            _view.ComboCounter.Construct(heroAttack, heroHealth);
            _view.LootCounter.Construct(_gameData.PlayerData.WorldData);
            _view.HudSkillContainer.Construct(heroSkills);
            _view.GetComponent<EntityUI>().Construct(heroHealth);
           



        }
    }
}
