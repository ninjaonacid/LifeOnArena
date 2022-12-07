using System;
using System.Collections.Generic;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;

namespace Code.Data
{
    [Serializable]
    public class SkillHudData
    {
        public event Action<HeroAbility> WeaponSkillChanged;

        public Dictionary<SkillSlotID, AbilityId> SlotSkill;

        public SkillHudData()
        {
            SlotSkill = new Dictionary<SkillSlotID, AbilityId>();
        }

        public void ChangeWeaponSkill(HeroAbility heroAbility)
        {
            if (SlotSkill.TryAdd(heroAbility.SkillSlotID, heroAbility.AbilityId))
                SlotSkill[heroAbility.SkillSlotID] = heroAbility.AbilityId;


            WeaponSkillChanged?.Invoke(heroAbility);
        }

    }

}