using System;
using Code.Logic.EntitiesComponents;
using Code.StaticData.StatSystem;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        private float _current;
        private float _max;
        [SerializeField] private StatController _stats;
        public Health Health
        {
            get => ((Health)_stats.Stats["Health"]);
            set {} 
        }
        public EnemyAnimator Animator;

        public event Action HealthChanged;
        
        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        private void OnDisable()
        {
            Health.ResetHealth();
        }

        private void OnEnable()
        {
            HealthChanged?.Invoke();
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