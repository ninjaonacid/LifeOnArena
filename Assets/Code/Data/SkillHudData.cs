using System;
using System.Collections.Generic;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;

namespace Code.Data
{
    [Serializable]
    public class SkillHudData
    {

 
        public Dictionary<AbilitySlotID, HeroAbilityId> SlotSkill;

        public SkillHudData()
        {
            SlotSkill = new Dictionary<AbilitySlotID, HeroAbilityId>();
        }

    
    }

}