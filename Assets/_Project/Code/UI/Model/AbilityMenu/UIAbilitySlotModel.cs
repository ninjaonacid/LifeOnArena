using System;
using Code.ConfigData.Ability;

namespace Code.UI.Model.AbilityMenu
{
    [Serializable]
    public class UIAbilitySlotModel
    {
        public bool IsEquipped;
        public string AbilityId;
        public AbilityTemplateBase Ability;

        public UIAbilitySlotModel(string abilityId)
        {
            AbilityId = abilityId;
        }
    }
}
