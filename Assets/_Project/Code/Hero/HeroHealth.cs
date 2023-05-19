using System;
using Code.Logic.EntitiesComponents;
using Code.StaticData.StatSystem;
using UnityEngine;

namespace Code.Hero
{
    public class HeroHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private HeroStats _stats;
        public Health Health
        {
            get => ((Health)_stats.Stats["Health"]);
            set {} 
        }
        
        public void TakeDamage(int damage)
        {
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