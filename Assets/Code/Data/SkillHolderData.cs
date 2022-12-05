using System;
using Code.StaticData.Ability;

namespace Code.Data
{
    [Serializable]
    public class SkillHolderData
    {
        public event Action<HeroAbility_SO> SkillChanged;

        public SkillHolderItemData SkillHolderItemData;
        public void ChangeSkillUI(HeroAbility_SO heroAbility)
        {
            SkillChanged?.Invoke(heroAbility);
        }

        public AbilityId[] AbilityID = new AbilityId[3];
        
    }

    public class SkillHolderItemData
    {
        public AbilityId AbilityId;
        public int SlotId;
    }
}