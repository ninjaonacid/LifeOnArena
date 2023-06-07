using Code.StaticData.Ability;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

namespace Code.UI.HUD.Skills
{
    public class HudSkillButton : OnScreenButton
    {
        public ButtonInputUI ButtonInput;

        public AbilitySlotID abilitySlotID;

        public AbilityTemplateBase heroAbility;

        public PlayerInput Input;

        [SerializeField] private HudSkillIcon _skillIcon;

        private void Awake()
        {
            _skillIcon.Image.enabled = false;
        }

        public void UpdateSkillView()
        {
            if (heroAbility != null)
            {
                _skillIcon.Image.sprite = heroAbility.Icon;
                _skillIcon.Image.enabled = true;
            }
        }
    }
}
