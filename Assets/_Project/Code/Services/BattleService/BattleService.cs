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
        private Collider[] _hits;

        private int FindTargets(Vector3 startPoint, float attackRadius, int hits, LayerMask mask)
        {
            _hits = new Collider[hits];

            return Physics.OverlapSphereNonAlloc(
                startPoint,
                attackRadius,
                _hits,
                mask);
        }

        public void AoeAttack(float damage, float radius, int maxTargets, Vector3 worldPoint, LayerMask mask)
        {
            for (int i = 0; i < FindTargets(worldPoint, radius, maxTargets, mask); i++)
            {
                if(_hits[i].transform.parent.TryGetComponent(out IDamageable health))
                {
                    Debug.Log("Zalupa");
                    health.TakeDamage(new HealthModifier()
                    {
                        
                    });
                }
            }
        }

        public void ApplyDamage(GameObject attacker, GameObject target)
        {
            var damageable = target.GetComponent<IDamageable>();
            IDamage damage = new HealthModifier
            {
                Attacker = attacker,
                IsCriticalHit = false,
                Magnitude = attacker.GetComponent<StatController>().Stats["Attack"].Value,
                OperationType = ModifierOperationType.Additive,
                Source = attacker.GetComponent<HeroWeapon>().GetEquippedWeapon()
            };
            
            damageable.TakeDamage(damage);
        }
        
        public void CreateAttack(GameObject attacker, GameObject target)
        {
            var attackerStats = attacker.GetComponent<StatController>();
            var targetStats = target.GetComponent<StatController>();

            var targetHealth = targetStats.Stats["Health"] as Attribute;
            
        }
    }
}
