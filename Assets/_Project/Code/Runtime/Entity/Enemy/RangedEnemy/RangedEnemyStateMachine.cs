﻿using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.Enemy.CommonEnemy;
using Code.Runtime.Modules.StateMachine;
using Code.Runtime.Modules.StateMachine.Transitions;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy.RangedEnemy
{
    public class RangedEnemyStateMachine : EnemyStateMachine
    {
        [SerializeField] private EnemyCastComponent _enemyCastComponent;
        protected override void Start()
        {
            base.Start();
            
            _enemyHealth.Health.CurrentValueChanged += TriggerDamageState;
            
            _fsm.AddState(nameof(RangedEnemyAttackState), 
                new RangedEnemyAttackState(_enemyAnimator, 
                    _animationData, 
                    _abilityController, 
                    _agentMoveToPlayer,
                    _enemyAttackComponent,
                    _enemyTarget, _enemyCastComponent, needExitTime: true));
            
            _fsm.AddTransitionFromAny(new Transition(
                "", 
                nameof(EnemyDeathState), 
                (transition) => _enemyDeath.IsDead, true));
            
            _fsm.AddTransition(new Transition(
                nameof(EnemyDeathState), 
                nameof(EnemyIdleState),
                (transition) => !_enemyDeath.IsDead));

            _fsm.AddTransition(new Transition(
                nameof(EnemyIdleState),
                nameof(EnemyChaseState),
                (transition) => _enemyTarget.HasTarget() && !_enemyCastComponent.TargetInAttackRange));

            _fsm.AddTransition(new TransitionAfter(
                nameof(RangedEnemyAttackState),
                nameof(EnemyIdleState),
                _animationData.Animations[AnimationKey.SpellCast].Length));
                
            _fsm.AddTransition(new Transition(
                nameof(EnemyChaseState),
                nameof(EnemyIdleState),
                (transition) => !_enemyTarget.HasTarget() || _enemyCastComponent.TargetInAttackRange));

            _fsm.AddTriggerTransitionFromAny("OnDamage", 
                new Transition(
                    " ", 
                    nameof(EnemyStaggerState), isForceTransition: true
                ));
          
            _fsm.AddTransition(new TransitionAfter(
                nameof(EnemyStaggerState),
                nameof(EnemyIdleState),
                _statController.Stats["HitRecovery"].Value
            ));

            _fsm.AddTransition(new Transition(
                nameof(EnemyChaseState),
                nameof(RangedEnemyAttackState),
                (transition) => _enemyCastComponent.CanAttack()));

            _fsm.AddTransition(new TransitionAfter(
                nameof(RangedEnemyAttackState),
                nameof(EnemyIdleState), 
                _animationData.Animations[AnimationKey.SpellCast].Length));
            
            _fsm.AddTransition(new Transition(
                nameof(EnemyIdleState),
                nameof(RangedEnemyAttackState),
                (transition) => _enemyCastComponent.CanAttack(),
                true));
            
            _fsm.AddTransitionFromAny(new Transition(
                "",
                nameof(EnemyStunnedState),
                (transition) => _tagController.HasTag("Stun"),
                true));
            
            _fsm.AddTransition(new Transition(
                nameof(EnemyStunnedState),
                nameof(EnemyIdleState),
                (transition) => !_tagController.HasTag("Stun")));

            _fsm.InitStateMachine();
        }


        private void TriggerDamageState()
        {
            _fsm.Trigger("OnDamage");
        }
    }
}