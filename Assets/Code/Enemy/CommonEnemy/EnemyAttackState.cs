using UnityEngine;

namespace Code.Enemy.CommonEnemy
{
    public class EnemyAttackState : CommonEnemyState
    {
        private readonly EnemyAttack _enemyAttack;
        private readonly AgentMoveToPlayer _agentMoveToPlayer;
     
        public EnemyAttackState(
            EnemyAnimator enemyAnimator, 
            EnemyAttack enemyAttack, 
            AgentMoveToPlayer agentMoveToPlayer,

            bool needsExitTime, bool isGhostState = false) : base(enemyAnimator, needsExitTime, isGhostState)
        {
            _enemyAttack = enemyAttack;
            _agentMoveToPlayer = agentMoveToPlayer;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _enemyAnimator.PlayAttack();

            _agentMoveToPlayer.ShouldMove(false);

        }

        public override void OnLogic()
        {
            base.OnLogic();
            
            _enemyAttack.LookAtTarget();
     
        }

        public override void OnExit()
        {
            base.OnExit();

        }

        public override void OnExitRequest()
        {
   
        }





    }
}
