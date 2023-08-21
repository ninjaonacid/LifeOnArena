using Code.Logic.Damage;
using Code.Logic.EntitiesComponents;
using Code.StaticData.StatSystem;
using UnityEngine;

namespace Code.Entity.Hero
{
    public class HeroHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private HeroStats _stats;
        public Health Health => ((Health)_stats.Stats["Health"]);
        
        public void TakeDamage(IDamage damage)
        {
            if (Health.CurrentValue <= 0)
            {
                return;
            }

            Health.ApplyModifier(new StatModifier()
            {
                Magnitude = -1 * damage.Magnitude,
                OperationType = ModifierOperationType.Additive
                
            });
        }
        
    }
}