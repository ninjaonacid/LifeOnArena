using UnityEngine;

namespace Code.Enemy.CommonEnemy
{
    public class EnemyIdleState : CommonEnemyState
    {
        
        public EnemyIdleState(EnemyAnimator enemyAnimator, bool needsExitTime, bool isGhostState = false) : base(enemyAnimator, needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _enemyAnimator.PlayIdle();

        }

        public override void OnLogic()
        {
            base.OnLogic();
            
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
        }

        public override bool IsStateOver()
        {
            return base.IsStateOver();
        }
    }
}
