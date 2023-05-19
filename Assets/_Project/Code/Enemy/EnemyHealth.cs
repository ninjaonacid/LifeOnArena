using System;
using Code.Logic.EntitiesComponents;
using Code.StaticData.StatSystem;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private StatController _stats;
        public Health Health
        {
            get => ((Health)_stats.Stats["Health"]);
            set {} 
        }
        public EnemyAnimator Animator;
        
        
        private void OnDisable()
        {
            Health.ResetHealth();
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