using System;
using System.Collections.Generic;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Modules.Requirements;
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
        public List<IRequirement> Requirements;

        [JsonIgnore]
        public ActiveAbilityBlueprintBase ActiveAbilityBlueprintBase;
        
    }
}
