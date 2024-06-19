using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.Enemy.MeleeEnemy;
using Code.Runtime.Entity.Enemy.RangedEnemy;
using Code.Runtime.Modules.StateMachine;
using Code.Runtime.Modules.StateMachine.Transitions;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy.BossEnemy.LordSkeleton
{
    public class SkeletonLordStateMachineHandler : EnemyStateMachine
    {
        protected override void Start()
        {
            base.Start();
            
            _fsm.AddState(nameof(EnemyCastState), 
                new EnemyCastState(_enemyAnimator, 
                    _animationData, 
                    _abilityController, 
                    NavMeshMoveToPlayer,
                    _enemyAttackComponent,
                    _enemyTarget, _enemyCastComponent, needExitTime: true));
            
            _fsm.AddState(nameof(MeleeEnemyAttackState), new MeleeEnemyAttackState(
                _enemyAttackComponent,
                NavMeshMoveToPlayer,
                _enemyTarget,
                _enemyAnimator,
                _enemyWeapon,
                _animationData,
                true
            ));
            
            _fsm.AddTransition(new Transition(nameof(EnemyIdleState), 
                nameof(MeleeEnemyAttackState),
                (transition) => _enemyAttackComponent.CanAttack()
                ));


            _fsm.AddTransition(new Transition(
                nameof(EnemyIdleState),
                nameof(EnemyChaseState),
                (transition) => _enemyTarget.HasTarget() && !_enemyAttackComponent.TargetInMeleeAttackRange));
            
            _fsm.AddTransition(new Transition(
                nameof(EnemyChaseState), nameof(MeleeEnemyAttackState),
                (transition) => _enemyAttackComponent.CanAttack()));
            
            _fsm.AddTransition(new Transition(nameof(EnemyChaseState), nameof(EnemyCastState),
                (transition) => _enemyCastComponent.CanAttack() && !_enemyAttackComponent.TargetInMeleeAttackRange));
            
            _fsm.AddTransition(new Transition(nameof(EnemyCastState), nameof(EnemyIdleState)));

            _fsm.AddTransition(new TransitionAfter(
                nameof(MeleeEnemyAttackState),
                nameof(EnemyChaseState),
                _animationData.Animations[AnimationKey.Attack1].Length,
                (transition) => _enemyTarget.HasTarget() && !_enemyAttackComponent.TargetInMeleeAttackRange));
            
            _fsm.AddTransition(new Transition(
                nameof(MeleeEnemyAttackState),
                nameof(EnemyIdleState)));
            
            _fsm.AddTransitionFromAny(new Transition("", nameof(EnemyDeathState), 
                (condition) => _enemyDeath.IsDead , 
                isForceTransition: true));
            
            
            _fsm.InitStateMachine();
        }

        
    }
}
