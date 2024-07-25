using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Core.EventSystem;
using Code.Runtime.CustomEvents;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StatSystem.StatModifiers;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    [RequireComponent(typeof(CharacterAnimator))]
    [RequireComponent(typeof(HeroMovement))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMovement _heroMovement;
        [SerializeField] private AnimationDataContainer _heroAnimations;
        
        private bool _isDead;
        public bool IsDead => _isDead;
        
        private IEventSystem _eventSystem;

        [Inject]
        public void Construct(IEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public void Revive()
        {
            _isDead = false;
        }

        public void ForceDeath()
        {
            _health.TakeDamage(new HealthModifier
            {
                Magnitude = -100000
            });
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
            _animator.PlayAnimation(_heroAnimations.Animations[AnimationKey.Die].Hash);
        }
    }
}