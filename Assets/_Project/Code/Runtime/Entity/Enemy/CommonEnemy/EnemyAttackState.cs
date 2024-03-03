using System;
using Code.Runtime.Logic.Timer;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyAttackState : CommonEnemyState
    {
        private readonly EnemyAttack _enemyAttack;
        private readonly AgentMoveToPlayer _agentMoveToPlayer;
        private readonly EnemyTarget _enemyTarget;

        public EnemyAttackState(EnemyAttack enemyAttack, AgentMoveToPlayer agentMoveToPlayer, EnemyTarget enemyTarget, EnemyAnimator enemyAnimator, bool needsExitTime, bool isGhostState = false) : base(enemyAnimator, needsExitTime, isGhostState)
        {
            _enemyAttack = enemyAttack;
            _agentMoveToPlayer = agentMoveToPlayer;
            _enemyTarget = enemyTarget;
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
           
           _enemyTarget.RotationToTarget();
     
        }

        public override void OnExit()
        {
            base.OnExit();
            _enemyAttack.Attack();
            _enemyAttack.AttackEnded();
        }

        public override void OnExitRequest()
        {
   
        }





    }
}
