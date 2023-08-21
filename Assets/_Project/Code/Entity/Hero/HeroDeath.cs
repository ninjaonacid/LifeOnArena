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

        [Inject]
        public void Construct(ILevelEventHandler levelEventHandler)
        {
            _levelEventHandler = levelEventHandler;
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
            _levelEventHandler.HeroDeath();
            heroMovement.enabled = false;
            Animator.PlayDeath();
        }
    }
}