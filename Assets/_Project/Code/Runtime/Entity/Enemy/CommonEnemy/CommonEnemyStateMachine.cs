using Code.Runtime.Modules.StateMachine;
using Code.Runtime.Modules.StateMachine.Transitions;

namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class CommonEnemyStateMachine : EnemyStateMachine
    {
        private void Start()
        {
            _enemyHealth.Health.CurrentValueChanged += TriggerDamageState;
            

            _fsm.AddTransition(new Transition(
                nameof(EnemyIdleState),
                nameof(EnemyChaseState),
                (transition) => _aggression.HasTarget && !EnemyAttackComponent.TargetInAttackRange));

            _fsm.AddTransition(new TransitionAfter(
                nameof(EnemyAttackState),
                nameof(EnemyChaseState),
                _enemyConfig.AttackDuration,
                (transition) => _aggression.HasTarget && !EnemyAttackComponent.TargetInAttackRange));
                
            _fsm.AddTransition(new TransitionAfter(
                nameof(EnemyChaseState),
                nameof(EnemyIdleState),
                _aggression.Cooldown,
                (transition) => !_aggression.HasTarget));

            _fsm.AddTriggerTransitionFromAny("OnDamage", 
                new Transition(
                    " ", 
                    nameof(EnemyStaggerState)
                ));
            
            _fsm.AddTransition(new TransitionAfter(
                nameof(EnemyStaggerState),
                nameof(EnemyIdleState),
                _enemyConfig.HitStaggerDuration
            ));

            _fsm.AddTransition(new Transition(
                nameof(EnemyChaseState),
                nameof(EnemyAttackState),
                (transition) => EnemyAttackComponent.CanAttack()));

            _fsm.AddTransition(new TransitionAfter(
                nameof(EnemyAttackState),
                nameof(EnemyIdleState),
                _enemyConfig.AttackDuration
                ));

            
            _fsm.AddTransition(new Transition(
                nameof(EnemyIdleState),
                nameof(EnemyAttackState),
                (transition) => EnemyAttackComponent.CanAttack(),
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
