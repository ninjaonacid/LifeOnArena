using System;
using System.Collections.Generic;
using Code.UI.HUD.Skills;

namespace Code.Data
{
    [Serializable]
    public class SkillHudData
    {

 
        public Dictionary<AbilitySlotID, string> SlotSkill;

        public SkillHudData()
        {
            SlotSkill = new Dictionary<AbilitySlotID, string>();
        }
       
    
    }

}