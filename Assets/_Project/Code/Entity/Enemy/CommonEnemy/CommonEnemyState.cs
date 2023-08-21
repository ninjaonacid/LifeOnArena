using Code.Logic.StateMachine.Base;

namespace Code.Entity.Enemy.CommonEnemy
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
