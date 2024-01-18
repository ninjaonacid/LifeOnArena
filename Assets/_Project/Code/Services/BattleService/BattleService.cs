using Code.ConfigData.StatSystem;
using Code.ConfigData.StatSystem.StatModifiers;
using Code.Entity;
using Code.Entity.EntitiesComponents;
using Code.Logic.Damage;
using Code.Logic.EntitiesComponents;
using UnityEngine;

namespace Code.Services.BattleService
{
    public class BattleService : IBattleService
    {
        private const int MaxTargets = 10;
        private readonly Collider[] _hits = new Collider[MaxTargets];

        private int FindTargets(Vector3 startPoint, float attackRadius, LayerMask mask, int hits = 10)
        {
            return Physics.OverlapSphereNonAlloc(
                startPoint,
                attackRadius,
                _hits,
                mask);
        }

        public int CreateAttack(StatController attackerStats, Vector3 attackPoint, LayerMask mask)
        {
            var attackRadius = attackerStats.Stats["AttackRadius"].Value;

            var hits = FindTargets(attackPoint, attackRadius, mask);
            
            for (int i = 0; i < hits; i++)
            {
                var target = _hits[i].gameObject;
                
                if(target)
                {
                    ApplyDamage(attackerStats, target);
                }
            }

            return hits;
        }
        
        public void ApplyDamage(StatController attacker, GameObject target)
        {
            var damageable = target.GetComponentInParent<IDamageable>();
            
            IDamage damage = new HealthModifier
            {
                Attacker = attacker.gameObject,
                IsCriticalHit = false,
                Magnitude = attacker.Stats["Attack"].Value * -1,
                OperationType = ModifierOperationType.Additive,
                Source = attacker.GetComponent<EntityWeapon>().GetEquippedWeapon()
            };
            
            damageable.TakeDamage(damage);
        }
        
    }
}
