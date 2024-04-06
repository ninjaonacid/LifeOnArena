using Code.Runtime.Core.EventSystem;
using Code.Runtime.CustomEvents;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    [RequireComponent(typeof(HeroAnimator))]
    [RequireComponent(typeof(HeroMovement))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMovement _heroMovement;
        
        private bool _isDead;
        public bool IsDead => _isDead;
        
        private IEventSystem _eventSystem;

        [Inject]
        public void Construct(IEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }
        private void Start()
        {
            _health.Health.CurrentValueChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            _health.Health.CurrentValueChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (!_isDead && _health.Health.CurrentValue <= 0) Die();
        }

        private void Die()
        {
            _isDead = true;
            _eventSystem.FireEvent(new HeroDeadEvent());
            _heroMovement.enabled = false;
        }
    }
}