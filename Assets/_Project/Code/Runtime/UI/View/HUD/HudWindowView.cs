using Code.Runtime.Entity.Hero;
using Code.Runtime.UI.Buttons;
using Code.Runtime.UI.View.HUD.Skills;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.HUD
{
    public class HudWindowView : BaseWindowView
    {
        public HudSkillContainer HudSkillContainer;
        public ComboCounter ComboCounter;
        public LootCounter LootCounter;
        public MusicButton MusicButton;
        public SoundButton SoundButton;
        public Button RestartButton;
        
        private HeroAttackComponent _heroAttackComponent;
        private HeroHealth _heroHealth;
    }
}
