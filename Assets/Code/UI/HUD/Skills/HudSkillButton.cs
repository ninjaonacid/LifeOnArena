using Code.Data;
using Code.Hero;
using Code.StaticData.Ability;
using UnityEngine;

namespace Code.UI.HUD.Skills
{
    public class HudSkillButton : MonoBehaviour
    {
        public AbilityId AbilityId;
        public SkillSlotID SkillSlotID;

        public HeroAbility HeroAbility;

        private HudSkillIcon _skillIcon;

        private void Awake()
        {
            _skillIcon = GetComponentInChildren<HudSkillIcon>();
            _skillIcon.Image.enabled = false;
        }

        public void UpdateSkill()
        {
            if (HeroAbility != null)
            {
                AbilityId = HeroAbility.AbilityId;
                _skillIcon.Image.sprite = HeroAbility.SkillIcon;
                _skillIcon.Image.enabled = true;
            }
        }



    }
}
