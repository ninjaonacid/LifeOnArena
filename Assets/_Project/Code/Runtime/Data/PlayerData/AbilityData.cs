using System;
using System.Collections.Generic;
using Code.Runtime.UI.Model.AbilityMenu;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class AbilityData
    {
        public List<UIAbilityModel> UnlockedAbilities = new();
        public List<UIAbilityModel> EquippedAbilities = new();
        public Queue<int> SkillIds = new Queue<int>();
        
    }

}