using UnityEngine;

namespace Code.Runtime.Entity.Enemy.RangedEnemy
{
    public class CheckCastRange : CheckAttackRange
    {
        [SerializeField] private EnemyCastComponent _castComponent;

        protected override void AttackRangeExit()
        {
            _castComponent.DisableCast();
        }

        protected override void AttackRangeEnter()
        {
            _castComponent.EnableCast();
        }
    }
}