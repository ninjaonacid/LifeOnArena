using Code.Logic;
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
                if(_hits[i].transform.parent.TryGetComponent(out IHealth health))
                {
                    Debug.Log("Zalupa");
                    health.TakeDamage(damage);
                }
            }
        }

    }
}