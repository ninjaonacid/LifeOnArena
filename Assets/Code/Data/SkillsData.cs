using System;

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