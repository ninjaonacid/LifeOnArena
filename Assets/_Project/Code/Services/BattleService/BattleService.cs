using Code.Entity;
using Code.Entity.Hero;
using Code.Logic.Damage;
using Code.Logic.EntitiesComponents;
using Code.StaticData.StatSystem;
using Code.StaticData.StatSystem.StatModifiers;
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
        
        public void AoeAttack(float damage, float radius, int maxTargets, Vector3 worldPoint, LayerMask mask)
        {
            for (int i = 0; i < FindTargets(worldPoint, radius, mask); i++)
            {
                if(_hits[i].transform.parent.TryGetComponent(out IDamageable health))
                {
                    
                }
            }
        }
        
        public void CreateAttack(StatController attackerStats, Vector3 attackPoint, LayerMask mask)
        {
            var attackRadius = attackerStats.Stats["AttackRadius"].Value;
            
            for (int i = 0; i < FindTargets(attackPoint, attackRadius, mask); i++)
            {
                var target = _hits[i].gameObject;
                
                if(target)
                {
                    ApplyDamage(attackerStats, target);
                }
            }
        }

        private void ApplyDamage(StatController attacker, GameObject target)
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
