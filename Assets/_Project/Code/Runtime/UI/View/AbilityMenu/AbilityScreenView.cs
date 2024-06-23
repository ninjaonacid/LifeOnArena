using Code.Runtime.Modules.WindowAnimations;
using UnityEngine;

namespace Code.Runtime.UI.View.AbilityMenu
{
    public class AbilityScreenView : BaseWindowView
    {
        public AbilityContainer AbilityContainer;
        public EquipSkillButton EquipButton;
        public UnEquipSkillButton UnEquipButton;
        public UnlockButton UnlockButton;
        public AbilityDescriptionArea AbilityDescription;
        public ResourcesCountUI ResourcesCount;
        
        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        public override void Close()
        {
            base.Close();
        }
    }
}
