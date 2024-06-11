using Code.Runtime.Modules.AbilitySystem;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.HUD.Skills
{
    public class HudSkillButton : OnScreenButton
    {
        [SerializeField] private AbilitySlotID _abilitySlotID;

        [SerializeField] private ActiveAbility _heroActiveAbility;
        
        [SerializeField] private HudSkillIcon _skillIcon;

        [SerializeField] private CooldownUI _cooldown;

        private void Awake()
        {
            _skillIcon.Image.enabled = false;
        }

        public void UpdateSkillView()
        {
            if (_heroActiveAbility != null)
            {
                _skillIcon.Image.sprite = _heroActiveAbility.AbilityBlueprint.Icon;
                _skillIcon.Image.enabled = true;
            }
        }

        public AbilitySlotID GetSlotId()
        {
            return _abilitySlotID;
        }

        public void SetAbility(ActiveAbility activeAbility)
        {
            _heroActiveAbility = activeAbility;
        }

        public ActiveAbility GetAbility()
        {
            return _heroActiveAbility;
        }

        public void UpdateCooldownView()
        {
            _cooldown.UpdateCooldown(_heroActiveAbility).Forget();
        }
        
    }
}
    