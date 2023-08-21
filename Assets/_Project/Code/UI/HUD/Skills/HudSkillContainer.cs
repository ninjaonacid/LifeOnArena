using Code.Entity.Hero;
using UnityEngine;

namespace Code.UI.HUD.Skills
{
    public class HudSkillContainer : MonoBehaviour
    {
        [SerializeField] private HudSkillButton[] _skillButtons;

        private HeroSkills _heroSkills;

        public void Construct(HeroSkills heroSkills)
        {
            _heroSkills = heroSkills;
            _heroSkills.OnSkillChanged += SetSkillSlots;
            
            Debug.Log(_heroSkills.GetHashCode().ToString());
        }

        private void SetSkillSlots()
        {
            for (int i = 0; i < _skillButtons.Length; i++)
            {
                if (_skillButtons[i].abilitySlotID == _heroSkills.SkillSlots[i].AbilitySlotID)
                {
                    _skillButtons[i].heroAbility = _heroSkills.SkillSlots[i].AbilityTemplate;
                    _skillButtons[i].UpdateSkillView();
                }
            }
            
            Debug.Log(_heroSkills.GetHashCode().ToString());
            
        }

        private void LinkButtonKeys()
        {
            foreach (var skillSlot in _heroSkills.SkillSlots)
            {
                foreach (var hudSkillSlot in _skillButtons)
                {
                    if (skillSlot.AbilitySlotID == hudSkillSlot.abilitySlotID)
                    {
                        
                    }
                }
            }
        }
    }
}
