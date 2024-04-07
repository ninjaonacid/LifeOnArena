using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Modules.StateMachine.States;
using Code.Runtime.Modules.StatSystem;

namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyAttackState : CommonEnemyState
    {
        private readonly EnemyAttackComponent _enemyAttackComponent;
        private readonly AgentMoveToPlayer _agentMoveToPlayer;
        private readonly EnemyTarget _enemyTarget;
        private readonly StatController _statController;

        public EnemyAttackState(EnemyAttackComponent enemyAttackComponent, AgentMoveToPlayer agentMoveToPlayer,
            EnemyTarget enemyTarget, EnemyAnimator enemyAnimator, StatController statController,
            AnimationDataContainer animationData, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            enemyAnimator, animationData, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _enemyAttackComponent = enemyAttackComponent;
            _agentMoveToPlayer = agentMoveToPlayer;
            _enemyTarget = enemyTarget;
            _statController = statController;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            _enemyAnimator.PlayAnimation(_animationData.Animations[AnimationKey.Attack1].Hash);
            //_enemyAnimator.SetAttackSpeed("AttackSpeed", _enemyAttackComponent.GetAttacksPerSecond());
            _agentMoveToPlayer.ShouldMove(false);
        }

        public override void OnLogic()
        {
            base.OnLogic();

            // if (Timer.Elapsed * 2 >= ((_animationData.Animations[AnimationKey.Attack1].Length  *
            //                       (MathF.Abs(_enemyAttackComponent.GetAttacksPerSecond())))))
            // {
            //     _enemyAnimator.SetAttackSpeed("AttackSpeed", 10f);
            // };
            
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