using Code.Services;
using UnityEngine;
using VContainer;

namespace Code.Hero
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
            Health.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            Health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (!_isDead && Health.Current <= 0) Die();
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