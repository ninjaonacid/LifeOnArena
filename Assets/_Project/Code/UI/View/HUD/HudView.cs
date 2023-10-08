using Code.Entity.Hero;
using Code.UI.Buttons;
using Code.UI.HUD;
using Code.UI.HUD.Skills;

namespace Code.UI.View.HUD
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
