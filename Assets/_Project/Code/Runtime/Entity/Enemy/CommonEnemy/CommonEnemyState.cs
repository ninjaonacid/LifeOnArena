using System;
using Code.Runtime.Logic.Timer;
using Code.Runtime.Modules.StateMachine.Base;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class CommonEnemyState : StateBase
    {
        protected readonly EnemyAnimator _enemyAnimator;

        public CommonEnemyState(EnemyAnimator enemyAnimator, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _enemyAnimator = enemyAnimator;
        }
    }
}
