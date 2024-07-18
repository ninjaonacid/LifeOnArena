using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.ConfigData.Attack;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StateMachine.States;
using Code.Runtime.Utils;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy.MeleeEnemy
{
    public class MeleeEnemyAttackState : BaseEnemyState
    {
        private readonly EnemyAttackComponent _enemyAttackComponent;
        private readonly NavMeshMoveToPlayer _navMeshMoveToPlayer;
        private readonly EnemyWeapon _enemyWeapon;
        private AttackConfig _currentAttack;

        public MeleeEnemyAttackState(EnemyAttackComponent enemyAttackComponent, NavMeshMoveToPlayer navMeshMoveToPlayer,
            EnemyWeapon enemyWeapon, CharacterAnimator characterAnimator, AnimationDataContainer animationData,
            EnemyTarget enemyTarget, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            characterAnimator, animationData, enemyTarget, needExitTime, isGhostState, onEnter, onLogic, onExit,
            canExit)
        {
            _enemyAttackComponent = enemyAttackComponent;
            _navMeshMoveToPlayer = navMeshMoveToPlayer;
            _enemyWeapon = enemyWeapon;
        }

        public override void OnEnter() 
        {
            base.OnEnter();
            _currentAttack = _enemyWeapon.WeaponData.AttacksConfigs.GetRandomElement();
            _characterAnimator.PlayAnimation(_currentAttack.AnimationData.Hash);
            _characterAnimator.SetAttackAnimationSpeed(_enemyAttackComponent.GetAttacksPerSecond());
            _navMeshMoveToPlayer.ShouldMove(false);
            Debug.Log("MeleeSTATE ENTER");
        }

        public override void OnLogic()
        {
            base.OnLogic();

            if (Timer.Elapsed >= ((_currentAttack.AnimationData.Length / _enemyAttackComponent.GetAttacksPerSecond() * _currentAttack.AnimationSpeedUpThreshold)))
            {
                _characterAnimator.SetAttackAnimationSpeed(1f);
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _enemyWeapon.DisableWeaponCollider();
            _enemyAttackComponent.AttackEnded();
            _enemyAttackComponent.ClearCollisionData();
            Debug.Log("MeleeSTATE EXIT");
        }

        public override void OnExitRequest()
        {
            float animationLength = _currentAttack.AnimationData.Length;
            float animationSlowedPart = _currentAttack.AnimationSpeedUpThreshold;
            float attackSpeed = _enemyAttackComponent.GetAttacksPerSecond();

            float slowDuration = animationLength * animationSlowedPart / attackSpeed;
            float normalDuration = animationLength * (1 - animationSlowedPart);

            float totalDuration = slowDuration + normalDuration;

            if (Timer.Elapsed >= totalDuration)
            {
                fsm.StateCanExit();
            }
        }
    }
}