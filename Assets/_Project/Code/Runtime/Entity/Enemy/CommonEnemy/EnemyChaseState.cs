namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyChaseState : CommonEnemyState
    {
        private readonly AgentMoveToPlayer _moveToPlayer;
        public EnemyChaseState(EnemyAnimator enemyAnimator,  AgentMoveToPlayer moveToPlayer,
            bool needsExitTime, bool isGhostState = false) : base(enemyAnimator, needsExitTime, isGhostState)
        {
            _moveToPlayer = moveToPlayer;
        }

        public override void Init()
        {
            base.Init();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _enemyAnimator.Move();
            _moveToPlayer.ShouldMove(true);
        }

        public override void OnLogic()
        {
            base.OnLogic();
            
            _moveToPlayer.SetDestination();
            
        }

        public override void OnExit()
        {
            base.OnExit();
            _moveToPlayer.ShouldMove(false);
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