using System;

namespace Code.Data
{
    [Serializable]
    public class CharacterStats
    {
        public float CurrentHP;

        public float BaseMaxHP;
        public float BaseDamage;
        public float BaseAttackRadius;
        public float BaseArmor;

        public float DamageModifier = 3;
        public float AttackRadiusModifier = 1;
        public float MaxHPModifier = 3;
        public float ArmorModifier = 1;
        
        public void ResetHP()
        {
            CurrentHP = CalculateHeroHealth();
        }

        public float CalculateHeroDamage()
        {
            return BaseDamage * DamageModifier;
        }

        public float CalculateHeroHealth()
        {
            return BaseMaxHP * MaxHPModifier;
        }
        public void InitBaseStats(float baseHp, float baseArmor, float baseDamage, float attackRadius)
        {
            BaseMaxHP = baseHp;
            BaseArmor = baseArmor;
            BaseDamage = baseDamage;
            BaseAttackRadius = attackRadius;
        }
    }
}