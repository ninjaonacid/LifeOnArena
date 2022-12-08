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

        public HeroAbilityData heroAbilityData;

        private HudSkillIcon _skillIcon;

        private void Awake()
        {
            _skillIcon = GetComponentInChildren<HudSkillIcon>();
            _skillIcon.Image.enabled = false;
        }

        public void UpdateSkill()
        {
            if (heroAbilityData != null)
            {
                AbilityId = heroAbilityData.AbilityId;
                _skillIcon.Image.sprite = heroAbilityData.SkillIcon;
                _skillIcon.Image.enabled = true;
            }
        }



    }
}
