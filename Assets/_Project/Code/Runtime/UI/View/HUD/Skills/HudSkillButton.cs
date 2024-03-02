using Code.Runtime.Modules.AbilitySystem;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

namespace Code.Runtime.UI.View.HUD.Skills
{
    public class HudSkillButton : OnScreenButton
    {
        [SerializeField] private AbilitySlotID _abilitySlotID;

        [SerializeField] private ActiveAbilityBlueprintBase HeroActiveAbilityBlueprintBase;
        
        [SerializeField] private HudSkillIcon _skillIcon;

        [SerializeField] private CooldownUI _cooldown;

        private void Awake()
        {
            _skillIcon.Image.enabled = false;
        }

        public void UpdateSkillView()
        {
            if (HeroActiveAbilityBlueprintBase != null)
            {
                _skillIcon.Image.sprite = HeroActiveAbilityBlueprintBase.Icon;
                _skillIcon.Image.enabled = true;
            }
        }

        public AbilitySlotID GetSlotId()
        {
            return _abilitySlotID;
        }

        public void SetAbility(ActiveAbilityBlueprintBase activeAbilityBlueprintBase)
        {
            HeroActiveAbilityBlueprintBase = activeAbilityBlueprintBase;
        }

        public ActiveAbilityBlueprintBase GetAbility()
        {
            return HeroActiveAbilityBlueprintBase;
        }

        public void UpdateCooldownView()
        {
            _cooldown.UpdateCooldown(HeroActiveAbilityBlueprintBase).Forget();
        }
        
    }
}
    