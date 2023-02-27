using Code.Logic;
using UnityEngine;

namespace Code.Services
{
    public class BattleService
    {
        private Collider[] _hits = new Collider[5];

        private int FindTargets(Vector3 startPoint, float attackRadius, int hits, LayerMask mask)
        {
            return Physics.OverlapSphereNonAlloc(
                startPoint,
                attackRadius,
                _hits,
                mask);
        }

        private void ApplyDamage()
        {
            foreach (Collider collider in _hits)
            {
                if(collider.transform.TryGetComponent(out IHealth health))
                {
                    //health.TakeDamage();
                }
            }
        }
    }
}
