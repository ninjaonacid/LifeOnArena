using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Logic.Damage;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.Runtime;
using Code.Runtime.Modules.StatSystem.Runtime.StatModifiers;
using UnityEngine;

namespace Code.Runtime.Entity
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
