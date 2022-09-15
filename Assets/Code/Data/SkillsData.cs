using System;
using Code.StaticData.Ability;
using Code.UI.SkillsMenu;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class SkillsData
    {
        public AbilityId[] AbilityID = new AbilityId[3];
    }
}