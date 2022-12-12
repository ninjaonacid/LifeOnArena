using System;
using System.Collections.Generic;
using Code.Hero.Abilities;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;

namespace Code.Data
{
    [Serializable]
    public class SkillHudData
    {

 
        public Dictionary<SkillSlotID, AbilityId> SlotSkill;

        public SkillHudData()
        {
            SlotSkill = new Dictionary<SkillSlotID, AbilityId>();
        }

    
    }

}