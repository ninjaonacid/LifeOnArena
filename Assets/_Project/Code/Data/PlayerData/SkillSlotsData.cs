using System;
using System.Collections.Generic;
using Code.UI.Model.AbilityMenu;

namespace Code.Data.PlayerData
{
    [Serializable]
    public class SkillSlotsData
    {
        public List<AbilitySlotModel> AbilitySlots;
        public Queue<int> SkillIds = new Queue<int>();
        
    }

}