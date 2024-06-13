using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.MeleeEnemy
{
    public class MeleeEnemyAttackState : BaseEnemyState
    {
        private readonly EnemyAttackComponent _enemyAttackComponent;
        private readonly AgentMoveToPlayer _agentMoveToPlayer;
        private readonly EnemyTarget _enemyTarget;
        private readonly EnemyWeapon _enemyWeapon;

        public MeleeEnemyAttackState(EnemyAttackComponent enemyAttackComponent, AgentMoveToPlayer agentMoveToPlayer,
            EnemyTarget enemyTarget, EnemyAnimator enemyAnimator, EnemyWeapon enemyWeapon,
            AnimationDataContainer animationData, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            enemyAnimator, animationData, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _enemyAttackComponent = enemyAttackComponent;
            _agentMoveToPlayer = agentMoveToPlayer;
            _enemyTarget = enemyTarget;
            _enemyWeapon = enemyWeapon;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            _enemyAnimator.PlayAnimation(_animationData.Animations[AnimationKey.Attack1].Hash);
            _enemyAnimator.SetAttackAnimationSpeed(_enemyAttackComponent.GetAttacksPerSecond());
            _agentMoveToPlayer.ShouldMove(false);
        }

        public override void OnLogic()
        {
            base.OnLogic();

            if (Timer.Elapsed >= ((_animationData.Animations[AnimationKey.Attack1].Length /
                                   _enemyAttackComponent.GetAttacksPerSecond()) / 5f))
            {
                _enemyAnimator.SetAttackAnimationSpeed(1f);
                _enemyWeapon.EnableCollider(true);
            }
            
            
            //_enemyTarget.RotationToTarget();
        }

        public override void OnExit()
        {
            base.OnExit();
            _enemyWeapon.EnableCollider(false);
            _enemyAttackComponent.AttackEnded();
            _enemyAttackComponent.ClearCollisionData();
            
        }

        public override void OnExitRequest()
        {
            if ( Timer.Elapsed >= ((_animationData.Animations[AnimationKey.Attack1].Length /
                                    _enemyAttackComponent.GetAttacksPerSecond() / 5f) + 0.5f))
            {
                fsm.StateCanExit();
            }
        }
    }
}