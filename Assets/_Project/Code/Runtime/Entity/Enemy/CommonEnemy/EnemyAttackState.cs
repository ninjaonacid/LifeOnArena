using System;
using Code.Runtime.Logic.Timer;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyAttackState : CommonEnemyState
    {
        private readonly EnemyAttackComponent _enemyAttackComponent;
        private readonly AgentMoveToPlayer _agentMoveToPlayer;
        private readonly EnemyTarget _enemyTarget;

        public EnemyAttackState(EnemyAttackComponent enemyAttackComponent, AgentMoveToPlayer agentMoveToPlayer, EnemyTarget enemyTarget, EnemyAnimator enemyAnimator, bool needsExitTime, bool isGhostState = false) : base(enemyAnimator, needsExitTime, isGhostState)
        {
            _enemyAttackComponent = enemyAttackComponent;
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
            _enemyAttackComponent.Attack();
            _enemyAttackComponent.AttackEnded();
        }

        public override void OnExitRequest()
        {
   
        }





    }
}
