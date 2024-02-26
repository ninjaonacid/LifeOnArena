using System;
using Code.Runtime.ConfigData.Ability;
using Newtonsoft.Json;

namespace Code.Runtime.UI.Model.AbilityMenu
{
    [Serializable]
    public class UIAbilitySlotModel
    {
        public bool IsEquipped;
        public bool IsUnlocked;
        public string AbilityName;
        public int Price;
        public int AbilityId;
        
        [JsonIgnore]
        public AbilityTemplateBase Ability;
        
    }
}
