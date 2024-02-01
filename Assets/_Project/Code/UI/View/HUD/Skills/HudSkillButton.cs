using Code.ConfigData.Ability;
using Code.UI.HUD.Skills;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

namespace Code.UI.View.HUD.Skills
{
    public class HudSkillButton : OnScreenButton
    {
        public AbilitySlotID abilitySlotID;

        public AbilityTemplateBase heroAbility;
        
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
    