using Code.ConfigData.StatSystem;
using Code.ConfigData.StatSystem.StatModifiers;
using Code.Entity;
using Code.Entity.EntitiesComponents;
using Code.Entity.StatusEffects;
using Code.Logic.Damage;
using UnityEngine;

namespace Code.Services.BattleService
{
    public class BattleService
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
        
        public Collider[] FindTargets(Vector3 startPoint, float attackRadius, LayerMask mask)
        {
             int targets = Physics.OverlapSphereNonAlloc(
                startPoint,
                attackRadius,
                _hits,
                mask);

             if (targets > 0)
             {
                 return _hits;
             }

             return null;
        }
        public int CreateAoeAbility(StatController attackerStats, Vector3 castPoint, LayerMask mask)
        {
            var attackRadius = attackerStats.Stats["AttackRadius"].Value;

            var hits = FindTargets(castPoint, attackRadius, mask);
            
            for (int i = 0; i < hits.Length; i++)
            {
                var target = _hits[i].gameObject;
                
                if(target)
                {

                }
            }

            return hits.Length;
        }

        public void CreateAbilityAttack(StatController caster, StatusEffect effect, GameObject target)
        {
            var targetController = target.GetComponent<StatController>();
            
           
        }
        
        public void CreateWeaponAttack(StatController attacker, GameObject target)
        {
            var damageable = target.GetComponentInParent<IDamageable>();
            
            IDamage damage = new HealthModifier
            {
                Attacker = attacker.gameObject,
                IsCriticalHit = false,
                Magnitude = attacker.Stats["Attack"].Value * -1,
                OperationType = ModifierOperationType.Additive,
                Source = attacker.GetComponent<EntityWeapon>().GetEquippedWeaponData()
            };
            
            damageable.TakeDamage(damage);
        }

        
    }
}
