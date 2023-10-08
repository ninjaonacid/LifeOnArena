using Code.Core.Factory;
using Code.Entity.Hero;

namespace Code.UI.Model
{
    public class HudModel : IScreenModel
    {
        private readonly IHeroFactory _heroFactory;

        public HeroHealth HeroHealth;
        public HeroSkills HeroSkills;
        public HeroAttack HeroAttack;

        public HudModel(IHeroFactory heroFactory)
        {
            _heroFactory = heroFactory;
        }

        public void Initialize()
        {
            HeroHealth = _heroFactory.HeroGameObject.GetComponent<HeroHealth>();
            HeroSkills = _heroFactory.HeroGameObject.GetComponent<HeroSkills>();
            HeroAttack = _heroFactory.HeroGameObject.GetComponent<HeroAttack>();
        }
    }
}
