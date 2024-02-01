using Code.Entity.Hero;
using UnityEngine;

namespace Code.UI.View.HUD.Skills
{
    public class HudSkillContainer : MonoBehaviour
    {
        [SerializeField] private HudSkillButton[] _skillButtons;

        private HeroSkills _heroSkills;
        private HeroAbilityCooldown _heroCooldown;

        public void Construct(HeroSkills heroSkills, HeroAbilityCooldown heroCooldown)
        {
            _heroSkills = heroSkills;
            _heroCooldown = heroCooldown;
            _heroSkills.OnSkillChanged += SetSkillSlots;
            _heroSkills.OnAbilityUse += UpdateCooldownView;
        }

        private void UpdateCooldownView()
        {
            foreach (var button in _skillButtons)
            {
                if (_heroCooldown.IsOnCooldown(button.GetAbility()))
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
                    _skillButtons[i].SetAbility(_heroSkills.SkillSlots[i].AbilityTemplate);
                    _skillButtons[i].UpdateSkillView();
                }
            }
        }
    }
}
