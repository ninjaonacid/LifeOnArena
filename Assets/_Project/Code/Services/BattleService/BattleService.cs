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

        private int FindTargets(Vector3 startPoint, float attackRadius, int hits, LayerMask mask)
        {
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
                    
                }
            }
        }

        public void Attack()
        {
            //for(int i = 0; i < FindTargets())
        }
        
        public void CreateAttack(GameObject attacker, GameObject target)
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
        
    }
}
