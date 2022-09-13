using Code.Data;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.HUD
{
    public class HudSkillButton : MonoBehaviour
    {
        private HeroAbility_SO _heroAbility;
        private AbilityID _abilityId;
        private Image _image;
        public void Construct(HeroAbility_SO heroAbility)
        {
            _heroAbility = heroAbility;

            _image.sprite = _heroAbility.SkillIcon;
            _abilityId = _heroAbility.AbilityId;
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
        }
    }
}
