using System;
using System.Collections.Generic;
using Code.UI.Model.AbilityMenu;

namespace Code.Data.PlayerData
{
    [Serializable]
    public class SkillSlotsData
    {
        public List<UIAbilitySlotModel> AbilitySlots = new();
        public Queue<UIAbilitySlotModel> EquippedSlots = new();
        public Queue<int> SkillIds = new Queue<int>();
        
    }

}