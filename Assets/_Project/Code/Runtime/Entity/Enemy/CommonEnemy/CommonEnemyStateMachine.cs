using Code.Runtime.Logic.StateMachine;
using Code.Runtime.Logic.StateMachine.Transitions;

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
                (transition) => _aggression.HasTarget && !_enemyAttack.TargetInAttackRange));

            _fsm.AddTransition(new TransitionAfter(
                nameof(EnemyAttackState),
                nameof(EnemyChaseState),
                _enemyConfig.AttackDuration,
                (transition) => _aggression.HasTarget && !_enemyAttack.TargetInAttackRange));
                
            _fsm.AddTransition(new TransitionAfter(
                nameof(EnemyChaseState),
                nameof(EnemyIdleState),
                _aggression.Cooldown,
                (transition) => !_aggression.HasTarget));

            _fsm.AddTriggerTransitionFromAny("OnDamage", new Transition(
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
                (transition) => _enemyAttack.CanAttack()));

            _fsm.AddTransition(new TransitionAfter(
                nameof(EnemyAttackState),
                nameof(EnemyIdleState),
                _enemyConfig.AttackDuration
                ));

            
            _fsm.AddTransition(new Transition(
                nameof(EnemyIdleState),
                nameof(EnemyAttackState),
                (transition) => _enemyAttack.CanAttack(),
                true));


            _fsm.InitStateMachine();
        }


        private void TriggerDamageState()
        {
            _fsm.Trigger("OnDamage");
        }
    }
}
