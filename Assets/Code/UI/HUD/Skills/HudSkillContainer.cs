using Code.Hero;
using Code.Services.Input;
using UnityEngine;

namespace Code.UI.HUD.Skills
{
    public class HudSkillContainer : MonoBehaviour
    {
        private HudSkillButton[] _skillButtons;
        private HeroSkills _heroSkills;

        public void Construct(
            HeroSkills heroSkills,
            HeroAbilityCooldown heroAbilityCooldown,
                IInputService input)
        {
            _heroSkills = heroSkills;


            _skillButtons = GetComponentsInChildren<HudSkillButton>();

            foreach (var button in _skillButtons)
            {
                button.Construct(heroAbilityCooldown, input);
            }

            _heroSkills.OnSkillChanged += SetSkillSlots;

            SetSkillSlots();
        }

        private void SetSkillSlots()
        {
            for (int i = 0; i < _skillButtons.Length; i++)
            {
                if (_skillButtons[i].SkillSlotID == _heroSkills.SkillSlots[i].SkillSlotId)
                {
                    _skillButtons[i].AbilityId = _heroSkills.SkillSlots[i].AbilityId;
                    _skillButtons[i].heroAbility = _heroSkills.SkillSlots[i].Ability;
                    _skillButtons[i].UpdateSkillView();
                }
            }
        }
    }
}
