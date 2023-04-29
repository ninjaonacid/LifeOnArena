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
            SlotSkill = new Dictionary<AbilitySlotID, string>()
            {
                [AbilitySlotID.First] = "",
                [AbilitySlotID.Second] = ""
            };
        }

        public void EquipSkill(string id)
        {
            var slot = FindSlot();

        }

        public void UnEquipSkill(string id)
        {
        }

        public AbilitySlotID FindSlot()
        {
            foreach (var slot in SlotSkill)
            {
                if (slot.Value == null)
                {
                    return slot.Key;
                }
            }
            
            return AbilitySlotID.Second;
        }
    }

}