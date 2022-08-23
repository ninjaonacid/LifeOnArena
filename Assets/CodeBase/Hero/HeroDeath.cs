using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        private bool _isDead;
        public HeroAnimator Animator;
        public HeroAttack Attack;
        public HeroHealth Health;

        public HeroMovement heroMovement;

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

            heroMovement.enabled = false;
            Animator.PlayDeath();
        }
    }
}