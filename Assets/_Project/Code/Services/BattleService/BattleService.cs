using System.Collections.Generic;
using System.Linq;
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
        private readonly Collider[] _overlapBuffer = new Collider[MaxTargets];
        private readonly Collider[] _targets = new Collider[MaxTargets];

        private int FindTargets(Vector3 startPoint, float radius, LayerMask mask, int hits = 10)
        {
            return Physics.OverlapSphereNonAlloc(
                startPoint,
                radius,
                _overlapBuffer,
                mask);
        }

        
        public Collider[] GetTargetsInRadius(Vector3 startPoint, float radius, LayerMask mask)
        {
            int hits = FindTargets(startPoint, radius, mask);

            if (hits > 0)
            {
                return _overlapBuffer;
            }

            return null;
        }

        public void CreateOverlapAttack(StatController attackerStats, Vector3 startPoint,  LayerMask mask)
        {
            int hits = 0;
            if (attackerStats.Stats.TryGetValue("AttackRadius", out var attackRadius))
            {

                hits = FindTargets(startPoint, attackRadius.Value, mask);
            }

            if (hits > 0)
            {
                for (var i = 0; i < hits; i++)
                {
                    var hit = _overlapBuffer[i];
                    CreateWeaponAttack(attackerStats, hit.gameObject);
                }
            }
        }
        public int CreateAoeAbility(StatController attackerStats, IReadOnlyList<StatusEffect> effects,  Vector3 castPoint, float radius, LayerMask mask)
        {

            var hits = FindTargets(castPoint, radius, mask);
            
            for (int i = 0; i < hits; i++)
            {
                var target = _overlapBuffer[i].gameObject;
                var effectController = target.GetComponentInParent<StatusEffectController>();
                
                foreach (var effect in effects)
                {
                    effectController.ApplyEffectToSelf(effect);
                }
            }

            return hits;
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
