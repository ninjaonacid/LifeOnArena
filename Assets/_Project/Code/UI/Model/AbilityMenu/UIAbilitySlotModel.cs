using System;
using Code.ConfigData.Ability;
using Newtonsoft.Json;
using UnityEngine.Serialization;

namespace Code.UI.Model.AbilityMenu
{
    [Serializable]
    public class UIAbilitySlotModel
    {
        public bool IsEquipped;
        public string AbilityName;
        public int AbilityId;
        
        [JsonIgnore]
        public AbilityTemplateBase Ability;
        
    }
}
