using Code.Runtime.Entity.Hero;
using UnityEngine;

namespace Code.Runtime.UI.View.HUD.Skills
{
    public class HudSkillContainer : MonoBehaviour
    {
        [SerializeField] private HudSkillButton[] _skillButtons;

        private HeroAbilityController _heroAbilityController;
        private AbilityCooldownController _cooldownController;

        public void Construct(HeroAbilityController heroAbilityController, AbilityCooldownController cooldownController)
        {
            _heroAbilityController = heroAbilityController;
            _cooldownController = cooldownController;
            _heroAbilityController.OnSkillChanged += SetAbilityControllerSlots;
            _heroAbilityController.OnAbilityUse += UpdateCooldownView;
        }

        private void UpdateCooldownView()
        {
            foreach (var button in _skillButtons)
            {
                if (_cooldownController.IsOnCooldown(button.GetAbility()))
                {
                    button.UpdateCooldownView();
                }
            }
        }

        private void SetAbilityControllerSlots()
        {
            for (int i = 0; i < _skillButtons.Length; i++)
            {
                if (_skillButtons[i].GetSlotId() == _heroAbilityController.AbilitySlots[i].AbilitySlotID)
                {
                    _skillButtons[i].SetAbility(_heroAbilityController.AbilitySlots[i].Ability);
                    _skillButtons[i].UpdateSkillView();
                }
            }
        }
    }
}
