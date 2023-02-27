using Code.StateMachine.Base;
using UnityEngine;

namespace Code.Enemy.CommonEnemy
{
    public class CommonEnemyState : StateBase
    {

        protected EnemyAnimator _enemyAnimator;
        public CommonEnemyState(EnemyAnimator enemyAnimator, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _enemyAnimator = enemyAnimator;
        }
    }
}
