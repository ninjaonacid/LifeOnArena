using Code.Entity.Enemy.CommonEnemy;
using Code.Logic.StateMachine;
using Code.Logic.StateMachine.Transitions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Entity.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        private FiniteStateMachine _fsm;


        [SerializeField] private AgentMoveToPlayer _agentMoveToPlayer;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private Aggression _aggression;
        [SerializeField] private EnemyTarget _enemyTarget;
        [FormerlySerializedAs("EnemyDamageable")] [FormerlySerializedAs("_enemyHealth")] [SerializeField] private EnemyHealth EnemyHealth;
        [SerializeField] private EnemyConfig _enemyConfig;

        private const string ChaseState = "EnemyChaseState";
        private const string AttackState = "EnemyAttackState";
        private const string IdleState = "EnemyIdleState";
        private const string HitStaggerState = "EnemyHitStagger";

        private void Start()
        {
            EnemyHealth.Health.CurrentValueChanged += TriggerDamageState;

            _fsm = new FiniteStateMachine();

            _fsm.AddState(ChaseState, new EnemyChaseState(
                _enemyAnimator, 
                _agentMoveToPlayer, 
                false, 
                false));

            _fsm.AddState(HitStaggerState, new EnemyHitStaggerState(
                _enemyAnimator,
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
            false, true
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

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}
