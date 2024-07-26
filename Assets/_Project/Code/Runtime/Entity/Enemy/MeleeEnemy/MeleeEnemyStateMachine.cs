using Code.Runtime.Modules.StateMachine;
using Code.Runtime.Modules.StateMachine.Transitions;

namespace Code.Runtime.Entity.Enemy.MeleeEnemy
{
    public class MeleeEnemyStateMachine : EnemyStateMachine
    {
        protected override void Start()
        {
            base.Start();

            _enemyStaggerComponent.Staggered += TriggerStaggerState;
            
            _fsm.AddState(nameof(MeleeEnemyAttackState), new MeleeEnemyAttackState(
                _enemyAttackComponent,
                _navMeshMoveToPlayer,
                _enemyWeapon,
                _characterAnimator,
                _animationData,
                _enemyTarget,
                true
            ));
            
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
                (transition) => _enemyTarget.HasTarget() && !_enemyAttackComponent.TargetInMeleeAttackRange));

            _fsm.AddTransition(new Transition(
                nameof(EnemyIdleState),
                nameof(MeleeEnemyAttackState),
                (transition) => _enemyAttackComponent.CanAttack() && _enemyTarget.HasTarget(),
                true));
            

            _fsm.AddTransition(new Transition(
                nameof(EnemyChaseState),
                nameof(EnemyIdleState),
                (transition) => !_enemyTarget.HasTarget()));
            
            _fsm.AddTransition(new Transition(
                nameof(EnemyChaseState),
                nameof(EnemyIdleState),
                (transition) => _enemyAttackComponent.TargetInMeleeAttackRange));

            _fsm.AddTriggerTransitionFromAny("OnDamage", 
                new Transition(
                    " ", 
                    nameof(EnemyStaggerState),
                    isForceTransition: true
                ));

            _fsm.AddTransition(new Transition(
                nameof(EnemyStaggerState),
                nameof(EnemyIdleState)
            ));

            _fsm.AddTransition(new Transition(
                nameof(EnemyChaseState),
                nameof(MeleeEnemyAttackState),
                (transition) => _enemyAttackComponent.CanAttack()));

            _fsm.AddTransition(new Transition(
                nameof(MeleeEnemyAttackState),
                nameof(EnemyIdleState)));


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


        private void TriggerStaggerState()
        {
            _fsm.Trigger("OnDamage");
        }
    }
}
