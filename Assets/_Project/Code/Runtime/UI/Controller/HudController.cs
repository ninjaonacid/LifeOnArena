using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;
using Code.Runtime.UI.View.HUD;
using UnityEngine.Assertions;

namespace Code.Runtime.UI.Controller
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

            _heroFactory.HeroGameObject.TryGetComponent(out HeroAttack heroAttack);
            _heroFactory.HeroGameObject.TryGetComponent(out HeroSkills heroSkills);
            _heroFactory.HeroGameObject.TryGetComponent(out HeroHealth heroHealth);
            _heroFactory.HeroGameObject.TryGetComponent(out HeroAbilityCooldown heroCooldown);
            
            _view.ComboCounter.Construct(heroAttack, heroHealth);
            _view.LootCounter.Construct(_gameData.PlayerData.WorldData);
            _view.HudSkillContainer.Construct(heroSkills, heroCooldown);
            _view.GetComponent<EntityUI>().Construct(heroHealth);
            



        }
    }
}
