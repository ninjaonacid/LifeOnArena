using System;
using System.Collections.Generic;
using Code.UI.HUD.Skills;

namespace Code.Data
{
    [Serializable]
    public class SkillSlotsData
    {
        public Dictionary<AbilitySlotID, string> SlotSkill;
        public Queue<int> SkillIds = new Queue<int>();
        public SkillSlotsData()
        {
            SlotSkill = new Dictionary<AbilitySlotID, string>()
            {
                [AbilitySlotID.First] = "",
                [AbilitySlotID.Second] = ""
            };
        }
        
    }

}