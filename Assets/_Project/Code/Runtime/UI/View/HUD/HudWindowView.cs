using _Project.Code.Runtime.Core.InputSystem;
using Code.Runtime.UI.Buttons;
using Code.Runtime.UI.View.HUD.Skills;

namespace Code.Runtime.UI.View.HUD
{
    public class HudWindowView : BaseWindowView
    {
        public HudSkillContainer HudSkillContainer;
        public ComboCounter ComboCounter;
        public LootCounter LootCounter;
        public BaseButton SettingsButton;
        public BaseButton ControlsButton;
        public StatusBar StatusBar;
        public BossHudHealthBar BossHudHealthBar;
        public AnimatedButton PortalButton;
        public DynamicJoyStick Joystick;
    }
}
