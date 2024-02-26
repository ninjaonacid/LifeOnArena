using Code.Runtime.Entity.Hero;
using Code.Runtime.UI.Buttons;
using Code.Runtime.UI.View.HUD.Skills;

namespace Code.Runtime.UI.View.HUD
{
    public class HudView : BaseView
    {
        public HudSkillContainer HudSkillContainer;
        public ComboCounter ComboCounter;
        public LootCounter LootCounter;
        public MusicButton MusicButton;
        public SoundButton SoundButton;
        
        private HeroAttack _heroAttack;
        private HeroHealth _heroHealth;
    }
}
