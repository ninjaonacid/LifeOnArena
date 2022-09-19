using System;
using Code.StaticData.Ability;

namespace Code.Data
{
    [Serializable]
    public class SkillHolderData
    {
        public AbilityId[] AbilityID = new AbilityId[3];
    }
}