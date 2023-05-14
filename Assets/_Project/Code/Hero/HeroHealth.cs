using System;
using Code.Logic.EntitiesComponents;
using Code.StaticData.StatSystem;
using UnityEngine;
using Attribute = Code.StaticData.StatSystem.Attribute;

namespace Code.Hero
{
    public class HeroHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private HeroStats _stats;
        public event Action HealthChanged;

        public float Max { get; }
        public Attribute Health
        {
            get => ((Attribute)_stats.Stats["Health"]);
            set {} 
        }

        private float _currentHp;


        public float Current
        {
            get => _currentHp;
            set
            {
                if (_currentHp != value)
                {
                   // _characterStats.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }
        
        public void TakeDamage(int damage)
        {
            if (Current <= 0)
                return;
            Current -= damage;

            if (Health.CurrentValue <= 0)
            {
                return;
            }

            Health.ApplyModifier(new StatModifier()
            {
                Magnitude = -1 * damage,
                OperationType = ModifierOperationType.Additive
            });
        }
        
    }
}