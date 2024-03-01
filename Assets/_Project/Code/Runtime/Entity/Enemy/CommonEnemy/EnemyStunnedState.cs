namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyStunnedState : CommonEnemyState
    {
        public EnemyStunnedState(EnemyAnimator enemyAnimator, AgentMoveToPlayer moveAgent, 
            TagController tagController, bool needsExitTime, bool isGhostState = false) : base(enemyAnimator, needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnLogic()
        {
            base.OnLogic();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override bool IsStateOver()
        {
            return base.IsStateOver();
        }
    }
}