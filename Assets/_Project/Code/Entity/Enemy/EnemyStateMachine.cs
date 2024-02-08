using System;
using Code.ConfigData.StateMachine;
using Code.Logic.StateMachine;
using UnityEngine;

namespace Code.Entity.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        protected FiniteStateMachine _fsm;
        
        [SerializeField] protected AgentMoveToPlayer _agentMoveToPlayer;
        [SerializeField] protected EnemyAttack _enemyAttack;
        [SerializeField] protected EnemyAnimator _enemyAnimator;
        [SerializeField] protected Aggression _aggression;
        [SerializeField] protected EnemyTarget _enemyTarget;
        [SerializeField] protected EnemyHealth _enemyHealth;

        protected EnemyStateMachineConfig _enemyConfig;

        public void Construct(EnemyStateMachineConfig config)
        {
            _enemyConfig = config;
            _fsm = new FiniteStateMachine();
        }
        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}
