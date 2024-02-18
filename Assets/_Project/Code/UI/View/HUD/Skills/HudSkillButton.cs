using Code.ConfigData.Ability;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace Code.UI.View.HUD.Skills
{
    public class HudSkillButton : OnScreenButton
    {
        [SerializeField] private AbilitySlotID _abilitySlotID;

        [SerializeField] private AbilityTemplateBase _heroAbility;
        
        [SerializeField] private HudSkillIcon _skillIcon;

        [SerializeField] private CooldownUI _cooldown;

        private void Awake()
        {
            _skillIcon.Image.enabled = false;
        }

        public void UpdateSkillView()
        {
            if (_heroAbility != null)
            {
                _skillIcon.Image.sprite = _heroAbility.Icon;
                _skillIcon.Image.enabled = true;
            }
        }

        public AbilitySlotID GetSlotId()
        {
            return _abilitySlotID;
        }

        public void SetAbility(AbilityTemplateBase ability)
        {
            _heroAbility = ability;
        }

        public AbilityTemplateBase GetAbility()
        {
            return _heroAbility;
        }

        public void UpdateCooldownView()
        {
            _cooldown.UpdateCooldown(_heroAbility).Forget();
        }
        
    }
}
    