using System;
using Code.StaticData.Ability;
using Code.UI.SkillsMenu;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class SkillsData
    {
        public HeroAbility_SO[] HeroAbility = new HeroAbility_SO[3];
    }
}