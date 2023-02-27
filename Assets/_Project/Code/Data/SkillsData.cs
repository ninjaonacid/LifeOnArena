using System;
using System.Collections.Generic;
using Code.StaticData.Ability;

namespace Code.Data
{
    [Serializable]
    
    public class SkillsData
    {
        public SpinAttackSkill SpinAttack;
        public FastSlashSkill FastSlash;

        public SkillsData()
        {
            SpinAttack = new SpinAttackSkill();
            FastSlash = new FastSlashSkill();
        }
        
    }
}