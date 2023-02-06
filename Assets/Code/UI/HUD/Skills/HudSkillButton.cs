using Code.Hero;
using Code.Services.Input;
using Code.StaticData.Ability;
using SimpleInputNamespace;
using UnityEngine;

namespace Code.UI.HUD.Skills
{
    public class HudSkillButton : MonoBehaviour
    {
        public ButtonInputUI ButtonInput;
        private IInputService _input;

        public AbilitySlotID abilitySlotID;

        public AbilityBluePrintBase heroAbility;


        private HudSkillIcon _skillIcon;

        public void Construct(IInputService input)
        {
            
            _input = input;
        }


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
