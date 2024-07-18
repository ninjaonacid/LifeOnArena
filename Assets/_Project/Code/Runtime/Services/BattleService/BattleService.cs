using System.Collections.Generic;
using Code.Runtime.Entity;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Logic.Damage;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.StatModifiers;
using UnityEngine;

namespace Code.Runtime.Services.BattleService
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
        
        public (int hits, Collider[] colliders) GetTargetsInRadius(Vector3 startPoint, float radius, LayerMask mask)
        {
            int hits = FindTargets(startPoint, radius, mask);

            if (hits > 0)
            {
                return (hits, _overlapBuffer);
            }

            return (0, null);
        }
        
        public Transform FindNearestEnemy(Transform casterTransform, float searchRadius, LayerMask targetLayer)
        {
            var targets = GetTargetsInRadius(casterTransform.position, searchRadius, targetLayer);
            
            float minDistance = float.MaxValue;
            
            Transform nearestTransform = default;

            for (var index = 0; index < targets.hits; index++)
            {
                var targetCollider = targets.colliders[index];
                
                if (targetCollider != null)
                {
                    float distance = Vector3.Distance(casterTransform.position, targetCollider.transform.position);
                    
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestTransform = targetCollider.transform;
                    }
                }
            }

            return nearestTransform;
        }

        public Transform FindNearestEnemyInSight(Transform casterTransform, float searchRadius, float fieldOfViewAngle, LayerMask targetLayer)
        {
            var targets = GetTargetsInRadius(casterTransform.position, searchRadius, targetLayer);
            
            float minDistance = float.MaxValue;
            
            Transform nearestTransform = default;
            var casterForward = casterTransform.forward;

            for (var index = 0; index < targets.hits; index++)
            {
                var targetCollider = targets.colliders[index];
                
                if (targetCollider != null)
                {
                    var targetTransform = targetCollider.transform;
                    var directionToTarget = targetTransform.position - casterTransform.position;
                    var angle = Vector3.Angle(casterForward, directionToTarget);

                    if (angle <= fieldOfViewAngle * 0.5f)
                    {
                        float distance = Vector3.Distance(casterTransform.position, targetCollider.transform.position);
                    
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            nearestTransform = targetCollider.transform;
                        }
                    }
                    
                }
            }

            return nearestTransform;
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
        public int CreateAoeAbility(StatController attackerStats, IReadOnlyList<GameplayEffect> effects,  Vector3 castPoint, float radius, LayerMask mask)
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

            var entityWeapon = attacker.GetComponent<EntityWeapon>();
            var weaponDamage = entityWeapon.WeaponData;
            
            IDamage damage = new HealthModifier
            {
                Attacker = attacker.gameObject,
                IsCriticalHit = false,
                Magnitude = attacker.Stats["Attack"].Value * -1,
                OperationType = ModifierOperationType.Additive,
                Source = attacker.GetComponent<EntityWeapon>().WeaponData
            };
            
            damageable.TakeDamage(damage);
        }

        
    }
}
