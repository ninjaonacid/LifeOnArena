using System;
using Code.Runtime.ConfigData.StateMachine;
using Code.Runtime.Logic.Timer;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyAttackState : CommonEnemyState
    {
        private readonly EnemyAttackComponent _enemyAttackComponent;
        private readonly AgentMoveToPlayer _agentMoveToPlayer;
        private readonly EnemyTarget _enemyTarget;
        private readonly EnemyStateMachineConfig _enemyConfig;

        public EnemyAttackState(EnemyAttackComponent enemyAttackComponent, AgentMoveToPlayer agentMoveToPlayer,
            EnemyTarget enemyTarget, EnemyAnimator enemyAnimator, EnemyStateMachineConfig config, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            enemyAnimator, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _enemyAttackComponent = enemyAttackComponent;
            _agentMoveToPlayer = agentMoveToPlayer;
            _enemyTarget = enemyTarget;
            _enemyConfig = config;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _enemyAnimator.PlayAttack(_enemyConfig.AttackDuration);
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