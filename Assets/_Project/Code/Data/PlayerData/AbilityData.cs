using System;
using System.Collections.Generic;
using Code.UI.Model.AbilityMenu;

namespace Code.Data.PlayerData
{
    [Serializable]
    public class AbilityData
    {
        public List<UIAbilitySlotModel> AbilitySlots = new();
        public List<UIAbilitySlotModel> EquippedSlots = new();
        public Queue<int> SkillIds = new Queue<int>();
        
    }

}