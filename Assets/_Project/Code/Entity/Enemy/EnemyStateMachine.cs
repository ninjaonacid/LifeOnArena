using Code.ConfigData.StateMachine;
using Code.Entity.Enemy.CommonEnemy;
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
        [SerializeField] protected StatusEffectController _statusController;

        protected EnemyStateMachineConfig _enemyConfig;

        public void Construct(EnemyStateMachineConfig config)
        {
            _enemyConfig = config;
        }

        private void Awake()
        {
            _fsm = new FiniteStateMachine();
            
            _fsm.AddState(nameof(EnemyChaseState), new EnemyChaseState(
                _enemyAnimator, 
                _agentMoveToPlayer, 
                false, 
                false));

            _fsm.AddState(nameof(EnemyStaggerState), new EnemyStaggerState(
                _enemyAnimator,
                _statusController,
                false,
                true));
            
            _fsm.AddState(nameof(EnemyAttackState), new EnemyAttackState(
                _enemyAnimator,
                _enemyAttack,
                _enemyTarget,
                _agentMoveToPlayer, false, false));

            _fsm.AddState(nameof(EnemyIdleState), new EnemyIdleState(
                _enemyAnimator,
                _enemyTarget, 
                _aggression, false, false));
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}
