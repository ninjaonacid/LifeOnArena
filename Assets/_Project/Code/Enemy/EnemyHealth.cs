using System;
using Code.Logic;
using Code.Logic.EntitiesComponents;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _current;

        [SerializeField] private float _max;

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
            _current = _max;
            HealthChanged?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            HealthChanged?.Invoke();
        }
    }
}