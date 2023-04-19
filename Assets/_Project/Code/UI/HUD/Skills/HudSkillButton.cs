using Code.StaticData.Ability;
using SimpleInputNamespace;
using UnityEngine;

namespace Code.UI.HUD.Skills
{
    public class HudSkillButton : MonoBehaviour
    {
        public ButtonInputUI ButtonInput;

        public AbilitySlotID abilitySlotID;

        public AbilityTemplateBase heroAbility;

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
                _skillIcon.Image.sprite = heroAbility.Icon;
                _skillIcon.Image.enabled = true;
            }
        }

    }
}
