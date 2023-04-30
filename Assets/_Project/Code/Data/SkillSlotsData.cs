using System;
using System.Collections.Generic;
using Code.UI.HUD.Skills;

namespace Code.Data
{
    [Serializable]
    public class SkillSlotsData
    {
        public Dictionary<AbilitySlotID, string> SlotSkill;
        public Queue<string> SkillIds = new Queue<string>();
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