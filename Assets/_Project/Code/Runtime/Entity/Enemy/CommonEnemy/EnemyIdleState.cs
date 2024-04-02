namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyIdleState : CommonEnemyState
    {
        private readonly EnemyTarget _target;

        public EnemyIdleState(EnemyAnimator enemyAnimator, 
            EnemyTarget enemyTarget,
            bool needsExitTime, bool isGhostState = false) : base(enemyAnimator, needsExitTime, isGhostState)
        {
            _target = enemyTarget;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _enemyAnimator.PlayIdle();

        }

        public override void OnLogic()
        {
            base.OnLogic();

            _target.RotationToTarget();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
        }
    }
}
