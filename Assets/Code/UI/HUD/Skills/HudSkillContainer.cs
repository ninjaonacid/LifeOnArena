using Code.Hero;
using UnityEngine;

namespace Code.UI.HUD.Skills
{
    public class HudSkillContainer : MonoBehaviour
    {
        private HudSkillButton[] _skillButtons;
        private HeroSkills _heroSkills;
        public void Construct(HeroSkills heroSkills)
        {
            _heroSkills = heroSkills;
            _skillButtons = GetComponentsInChildren<HudSkillButton>();
            _heroSkills.OnSkillChanged += SetSkillSlots;
            SetSkillSlots();
        }

        private void SetSkillSlots()
        {
            for (int i = 0; i < _skillButtons.Length; i++)
            {
                if (_skillButtons[i].SkillSlotID == _heroSkills.SkillSlots[i].skillSlotId)
                {
                    _skillButtons[i].HeroAbility = _heroSkills.SkillSlots[i].ability;
                    _skillButtons[i].UpdateSkill();
                }
            }
        }
    }
}
