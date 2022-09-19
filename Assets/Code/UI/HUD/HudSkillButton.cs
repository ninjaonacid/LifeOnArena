using Code.StaticData.Ability;
using UnityEngine;

namespace Code.UI.HUD
{
    public class HudSkillButton : MonoBehaviour
    {
        private HeroAbility_SO _heroAbility;
        public AbilityId _abilityId;
        private  HudSkillIcon _skillIcon;
        public void Construct(HeroAbility_SO heroAbility)
        {
            _skillIcon = GetComponentInChildren<HudSkillIcon>();
            
            _heroAbility = heroAbility;
            if (heroAbility != null)
            {
                _abilityId = _heroAbility.AbilityId;
                _skillIcon.Image.sprite = _heroAbility.SkillIcon;
                _skillIcon.Image.enabled = true;
            }
            else
            {
                _skillIcon.Image.enabled = false;
            }
        }

    }
}
