using Code.Logic.StateMachine;
using Code.Logic.StateMachine.Transitions;

namespace Code.Entity.Enemy.CommonEnemy
{
    public class CommonEnemyStateMachine : EnemyStateMachine
    {
        private const string ChaseState = "EnemyChaseState";
        private const string AttackState = "EnemyAttackState";
        private const string IdleState = "EnemyIdleState";
        private const string HitStaggerState = "EnemyHitStagger";
        private void Start()
        {
            _enemyHealth.Health.CurrentValueChanged += TriggerDamageState;
            

            _fsm.AddState(ChaseState, new EnemyChaseState(
                _enemyAnimator, 
                _agentMoveToPlayer, 
                false, 
                false));

            _fsm.AddState(HitStaggerState, new EnemyStaggerState(
                _enemyAnimator,
                _statusController,
                false,
                true));
            
            _fsm.AddState(AttackState, new EnemyAttackState(
                _enemyAnimator,
                _enemyAttack,
                _enemyTarget,
                _agentMoveToPlayer, false, false));

            _fsm.AddState(IdleState, new EnemyIdleState(
                _enemyAnimator,
                _enemyTarget, 
                _aggression, false, false));

            _fsm.AddTransition(new Transition(
                IdleState,
                ChaseState,
                (transition) => _aggression.HasTarget && !_enemyAttack.TargetInAttackRange));

            _fsm.AddTransition(new TransitionAfter(
                AttackState,
                ChaseState,
                _enemyConfig.AttackDuration,
                (transition) => _aggression.HasTarget && !_enemyAttack.TargetInAttackRange));
                
            _fsm.AddTransition(new TransitionAfter(
                ChaseState,
                IdleState,
                _aggression.Cooldown,
                (transition) => !_aggression.HasTarget));

            _fsm.AddTriggerTransitionFromAny("OnDamage", new CycleTransition(
                    " ", 
                    HitStaggerState,
            true, true
            ));
            
            _fsm.AddTransition(new TransitionAfter(
                HitStaggerState,
                IdleState,
                _enemyConfig.HitStaggerDuration
            ));

            _fsm.AddTransition(new Transition(
                ChaseState,
                AttackState,
                (transition) => _enemyAttack.CanAttack()));

            _fsm.AddTransition(new TransitionAfter(
                AttackState,
                IdleState,
                _enemyConfig.AttackDuration
                ));

            
            _fsm.AddTransition(new Transition(
                IdleState,
                AttackState,
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
