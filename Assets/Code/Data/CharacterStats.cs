using System;

namespace Code.Data
{
    [Serializable]
    public class CharacterStats
    {
        public float CurrentLevel;
        public float Exp;

        public float BaseAttackSpeed;
        public float BaseDamage;
        public float BaseAttackRadius;
        public float GoldRate;
    }
}