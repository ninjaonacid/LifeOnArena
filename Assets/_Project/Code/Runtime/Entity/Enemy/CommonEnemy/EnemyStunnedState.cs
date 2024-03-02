namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyStunnedState : CommonEnemyState
    {
        private readonly TagController _tagController;

        public EnemyStunnedState(EnemyAnimator enemyAnimator, AgentMoveToPlayer moveAgent,
            TagController tagController, bool needsExitTime, bool isGhostState = false) : base(enemyAnimator,
            needsExitTime, isGhostState)
        {
            _tagController = tagController;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnLogic()
        {
            base.OnLogic();
            if (IsStateOver()) fsm.StateCanExit();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
            if (IsStateOver()) fsm.StateCanExit();
        }

        public override bool IsStateOver()
        {
            return !_tagController.HasTag("Stun");
        }
    }
}