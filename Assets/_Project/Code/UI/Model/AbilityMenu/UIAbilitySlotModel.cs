using System;
using Code.ConfigData.Ability;
using Newtonsoft.Json;

namespace Code.UI.Model.AbilityMenu
{
    [Serializable]
    public class UIAbilitySlotModel
    {
        public bool IsEquipped;
        public string AbilityId;
        
        [JsonIgnore]
        public AbilityTemplateBase Ability;

        public UIAbilitySlotModel(string abilityId)
        {
            AbilityId = abilityId;
        }
    }
}
