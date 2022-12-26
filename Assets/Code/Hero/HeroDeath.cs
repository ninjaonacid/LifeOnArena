using Code.Services;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        private bool _isDead;
        public HeroAnimator Animator;
        public HeroAttack Attack;
        public HeroHealth Health;

        public HeroMovement heroMovement;

        private IGameEventHandler _gameEventHandler;
        public void Construct(IGameEventHandler gameEventHandler)
        {
            _gameEventHandler = gameEventHandler;
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
            _gameEventHandler.HeroDeath();
            heroMovement.enabled = false;
            Animator.PlayDeath();
        }
    }
}