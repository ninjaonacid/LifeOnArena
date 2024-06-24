using System;
using Code.Runtime.Modules.AbilitySystem;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Localization;

namespace Code.Runtime.UI.Model.AbilityMenu
{
    [Serializable]
    public class AbilityModel
    {
        public bool IsEquipped;
        public bool IsUnlocked;
        public int AbilityId;
        public int Price;
        [JsonIgnore]
        public Sprite Icon;
        public AbilityTreeData AbilityTreeData;
        [JsonIgnore]
        public LocalizedString Description;
        
    }
}
