using Code.ConfigData.StatSystem;
using Code.ConfigData.StatSystem.StatModifiers;
using Code.Entity.EntitiesComponents;
using Code.Logic.Damage;
using UnityEngine;

namespace Code.Entity
{
    public class EntityHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private StatController _stats;
        public Health Health => _stats.Stats["Health"] as Health;
        
        public void TakeDamage(IDamage damage)
        {
            Health.ApplyModifier(new HealthModifier
            {
                Magnitude = damage.Magnitude,
                OperationType = ModifierOperationType.Additive,
                Source = damage.Source,
                Attacker = damage.Attacker,
                IsCriticalHit = damage.IsCriticalHit
            });
            
        }
    }
}
