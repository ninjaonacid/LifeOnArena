using Code.Entity.Hero;
using Code.UI.HUD;
using Code.UI.HUD.Skills;

namespace Code.UI.View.HUD
{
    public class HudView : BaseView
    {
        public HudSkillContainer HudSkillContainer;
        public ComboCounter ComboCounter;
        public LootCounter LootCounter;
        
        private HeroAttack _heroAttack;
        private HeroHealth _heroHealth;
    }
}
