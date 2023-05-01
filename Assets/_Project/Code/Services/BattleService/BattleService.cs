using Code.Logic;
using UnityEngine;

namespace Code.Services.BattleService
{
    public class BattleService : IBattleService
    {
        private Collider[] _hits;

        private int Hit(Vector3 startPoint, float attackRadius, int hits, LayerMask mask)
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
            for (int i = 0; i < Hit(worldPoint, radius, maxTargets, mask); i++)
            {
                ApplyDamage(damage);
            }
        }

        private void ApplyDamage(float damage)
        {
            foreach (Collider collider in _hits)
            {
                if(collider.transform.TryGetComponent(out IHealth health))
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }
}
