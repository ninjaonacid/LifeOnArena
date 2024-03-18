using Code.Runtime.Entity.Hero;
using UnityEngine;

namespace Code.Runtime.UI.View.HUD.Skills
{
    public class HudSkillContainer : MonoBehaviour
    {
        [SerializeField] private HudSkillButton[] _skillButtons;

        private HeroSkills _heroSkills;
        private AbilityCooldownController _cooldownController;

        public void Construct(HeroSkills heroSkills, AbilityCooldownController cooldownController)
        {
            _heroSkills = heroSkills;
            _cooldownController = cooldownController;
            _heroSkills.OnSkillChanged += SetSkillSlots;
            _heroSkills.OnAbilityUse += UpdateCooldownView;
        }

        private void UpdateCooldownView()
        {
            foreach (var button in _skillButtons)
            {
                if (_cooldownController.IsOnCooldown(button.GetAbilityBlueprint().GetAbility()))
                {
                    button.UpdateCooldownView();
                }
            }
        }

        private void SetSkillSlots()
        {
            for (int i = 0; i < _skillButtons.Length; i++)
            {
                if (_skillButtons[i].GetSlotId() == _heroSkills.SkillSlots[i].AbilitySlotID)
                {
                    _skillButtons[i].SetAbility(_heroSkills.SkillSlots[i].Ability);
                    _skillButtons[i].UpdateSkillView();
                }
            }
        }
    }
}
