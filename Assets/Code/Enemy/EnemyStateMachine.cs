using System;
using System.Net.Http.Headers;
using Code.Enemy.CommonEnemy;
using Code.StateMachine;
using Code.StateMachine.Transitions;
using UnityEngine;

namespace Code.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        private FiniteStateMachine _fsm;


        [SerializeField] private AgentMoveToPlayer _agentMoveToPlayer;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private Aggression _aggression;

        private const string ChaseState = "EnemyChaseState";
        private const string AttackState = "EnemyAttackState";
        private const string IdleState = "EnemyIdleState";
        private void Start()
        {
            _fsm = new FiniteStateMachine();

            _fsm.AddState(ChaseState, new EnemyChaseState(_enemyAnimator, _agentMoveToPlayer, false, false));
            _fsm.AddState(AttackState, new EnemyAttackState(_enemyAnimator,_enemyAttack, _agentMoveToPlayer, false, false));
            _fsm.AddState(IdleState, new EnemyIdleState(_enemyAnimator, false, false));

            _fsm.AddTransition(new Transition(
                IdleState,
                ChaseState,
                (transition) => _aggression.HasTarget && !_enemyAttack.TargetInAttackRange));

            _fsm.AddTransition(new Transition(
                AttackState,
                ChaseState,
                (transition) => _aggression.HasTarget && !_enemyAttack.TargetInAttackRange));
                
            _fsm.AddTransition(new TransitionAfter(
                ChaseState,
                IdleState,
                _aggression.Cooldown,
                (transition) => !_aggression.HasTarget));


            _fsm.AddTransition(new Transition(
                ChaseState,
                AttackState,
                (transition) => _enemyAttack.CanAttack()));

            _fsm.AddTransition(new TransitionAfter(
                AttackState,
                IdleState,
                _enemyAttack.AttackDuration
                ));

            
            _fsm.AddTransition(new Transition(
                IdleState,
                AttackState,
                (transition) => _enemyAttack.CanAttack(),
                true));


            _fsm.InitStateMachine();
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}
