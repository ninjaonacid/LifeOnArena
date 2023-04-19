using System;
using System.Collections.Generic;
using Code.UI.HUD.Skills;

namespace Code.Data
{
    [Serializable]
    public class SkillSlotsData
    {
        public Dictionary<AbilitySlotID, string> SlotSkill;

        public SkillSlotsData()
        {
            SlotSkill = new Dictionary<AbilitySlotID, string>();
        }

    }

}