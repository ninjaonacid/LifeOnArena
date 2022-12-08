using Code.Hero.Abilities;
using Code.StaticData.Ability;
using UnityEngine;

namespace Code.UI.HUD.Skills
{
    public class HudSkillButton : MonoBehaviour
    {
        public AbilityId AbilityId;
        public SkillSlotID SkillSlotID;

        public Ability heroAbility;

        private HudSkillIcon _skillIcon;

        private void Awake()
        {
            _skillIcon = GetComponentInChildren<HudSkillIcon>();
            _skillIcon.Image.enabled = false;
        }

        public void UpdateSkillView()
        {
            if (heroAbility != null)
            {
                _skillIcon.Image.sprite = heroAbility.AbilityIcon;
                _skillIcon.Image.enabled = true;
            }
        }



    }
}
