using System;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class CharacterStats
    {
        public float CurrentHP;
        public float BaseMaxHP;

        [SerializeField] private float _baseDamage;
        [SerializeField] private float _baseAttackRadius;
        [SerializeField] private float _baseArmor;

        public float DamageModifier = 1;
        public float AttackRadiusModifier = 1;
        public float MaxHPModifier = 1;
        public float ArmorModifier = 1;
        
        public void ResetHP()
        {
            CurrentHP = CalculateHeroHealth();
        }

        public float CalculateHeroDamage()
        {
            return _baseDamage * DamageModifier;
        }

        public float CalculateHeroHealth()
        {
            return BaseMaxHP * MaxHPModifier;
        }

        public float CalculateHeroAttackRadius()
        {
            return _baseAttackRadius * AttackRadiusModifier;
        }


        public void InitBaseStats(float baseHp, float baseArmor, float baseDamage, float attackRadius)
        {
            BaseMaxHP = baseHp;
            _baseArmor = baseArmor;
            _baseDamage = baseDamage;
            _baseAttackRadius = attackRadius;
        }
    }
}