using Code.Logic.Damage;
using Code.Logic.EntitiesComponents;
using Code.StaticData.StatSystem;
using Code.StaticData.StatSystem.StatModifiers;
using UnityEngine;

namespace Code.Entity.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private StatController _stats;
        public Health Health => ((Health)_stats.Stats["Health"]);
      

        public EnemyAnimator Animator;
        
        private void OnDisable()
        {
            Health.ResetHealth();
        }
        
        public void TakeDamage(IDamage damage)
        {
            if (Health.CurrentValue <= 0)
            {
                return;
            }

            Health.ApplyModifier(new HealthModifier
            {
                Magnitude = -1 * damage.Magnitude,
                OperationType = ModifierOperationType.Additive
            });
        }
    }
}