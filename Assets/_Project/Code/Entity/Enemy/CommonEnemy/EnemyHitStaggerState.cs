namespace Code.Entity.Enemy.CommonEnemy
{
    public class EnemyHitStaggerState : CommonEnemyState
    {
        public float DisableDuration;
        public EnemyHitStaggerState(
            EnemyAnimator enemyAnimator,
            StatusEffectController statusController,
            bool needsExitTime, bool isGhostState = false) : base(enemyAnimator, needsExitTime, isGhostState)
        {

        }
        public override void OnEnter()
        {
            base.OnEnter();
            _enemyAnimator.PlayHit();
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
