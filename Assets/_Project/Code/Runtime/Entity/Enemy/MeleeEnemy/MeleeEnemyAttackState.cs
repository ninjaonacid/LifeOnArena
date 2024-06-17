using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.ConfigData.Attack;
using Code.Runtime.Modules.StateMachine.States;
using Code.Runtime.Utils;
using Random = UnityEngine.Random;

namespace Code.Runtime.Entity.Enemy.MeleeEnemy
{
    public class MeleeEnemyAttackState : BaseEnemyState
    {
        private readonly EnemyAttackComponent _enemyAttackComponent;
        private readonly NavMeshMoveToPlayer _navMeshMoveToPlayer;
        private readonly EnemyTarget _enemyTarget;
        private readonly EnemyWeapon _enemyWeapon;
        private AttackConfig _currentAttack;

        public MeleeEnemyAttackState(EnemyAttackComponent enemyAttackComponent, NavMeshMoveToPlayer navMeshMoveToPlayer,
            EnemyTarget enemyTarget, EnemyAnimator enemyAnimator, EnemyWeapon enemyWeapon,
            AnimationDataContainer animationData, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            enemyAnimator, animationData, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _enemyAttackComponent = enemyAttackComponent;
            _navMeshMoveToPlayer = navMeshMoveToPlayer;
            _enemyTarget = enemyTarget;
            _enemyWeapon = enemyWeapon;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _currentAttack = _enemyWeapon.WeaponData.AttacksConfigs.GetRandomElement();
            _enemyAnimator.PlayAnimation(_currentAttack.AnimationData.Hash);
            _enemyAnimator.SetAttackAnimationSpeed(_enemyAttackComponent.GetAttacksPerSecond());
            _navMeshMoveToPlayer.ShouldMove(false);
        }

        public override void OnLogic()
        {
            base.OnLogic();

            if (Timer.Elapsed >= ((_currentAttack.AnimationData.Length /
                                   _enemyAttackComponent.GetAttacksPerSecond()) / 5f))
            {
                _enemyAnimator.SetAttackAnimationSpeed(1f);
                //_enemyWeapon.EnableCollider(true);
            }
            
            
            //_enemyTarget.RotationToTarget();
        }

        public override void OnExit()
        {
            base.OnExit();
            _enemyWeapon.DisableWeaponCollider();
            _enemyAttackComponent.AttackEnded();
            _enemyAttackComponent.ClearCollisionData();
        }

        public override void OnExitRequest()
        {
            if ( Timer.Elapsed >= ((_currentAttack.AnimationData.Length /
                                    _enemyAttackComponent.GetAttacksPerSecond() / 5f) + 0.5f))
            {
                fsm.StateCanExit();
            }
        }
    }
}