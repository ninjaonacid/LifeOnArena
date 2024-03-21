using System;
using Code.Runtime.Modules.AbilitySystem;
using Newtonsoft.Json;

namespace Code.Runtime.UI.Model.AbilityMenu
{
    [Serializable]
    public class UIAbilityModel
    {
        public bool IsEquipped;
        public bool IsUnlocked;
        public string AbilityName;
        public int Price;
        public int AbilityId;
        
        [JsonIgnore]
        public ActiveAbilityBlueprintBase ActiveAbilityBlueprintBase;
        
    }
}
