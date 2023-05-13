using System;
using Code.Logic;
using Code.Logic.EntitiesComponents;
using Code.StaticData.StatSystem;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        private Stat _health;
        
        private float _current;
        private float _max;

        public Stat Health
        {
            set => _health = value;
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
        private void OnEnable()
        {
            _current = _health.Value;
            HealthChanged?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            HealthChanged?.Invoke();
        }
    }
}