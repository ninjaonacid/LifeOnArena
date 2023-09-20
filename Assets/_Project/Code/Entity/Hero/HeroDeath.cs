using Code.Core.EventSystem;
using Code.CustomEvents;
using Code.Services;
using UnityEngine;
using VContainer;

namespace Code.Entity.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroDeath : MonoBehaviour
    {
        private bool _isDead;
        public HeroAnimator Animator;
        public HeroHealth Health;

        public HeroMovement heroMovement;

        private ILevelEventHandler _levelEventHandler;
        private IEventSystem _eventSystem;

        [Inject]
        public void Construct(IEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }
        private void Start()
        {
            Health.Health.CurrentValueChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            Health.Health.CurrentValueChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (!_isDead && Health.Health.CurrentValue <= 0) Die();
        }

        private void Die()
        {
            _isDead = true;
            _eventSystem.FireEvent(new HeroDeadEvent());
            heroMovement.enabled = false;
            Animator.PlayDeath();
        }
    }
}