using System.Timers;
using Code.Runtime.ConfigData.StateMachine;
using Code.Runtime.Entity.Enemy.CommonEnemy;
using Code.Runtime.Modules.StateMachine;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        protected FiniteStateMachine _fsm;

        [SerializeField] protected AgentMoveToPlayer _agentMoveToPlayer;
        [SerializeField] protected EnemyAttackComponent EnemyAttackComponent;
        [SerializeField] protected EnemyAnimator _enemyAnimator;
        [SerializeField] protected EnemyTarget _enemyTarget;
        [SerializeField] protected EnemyHealth _enemyHealth;
        [SerializeField] protected TagController _tagController;

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
                true,
                false,
                canExit: (state) => state.Timer.Elapsed >= _enemyConfig.HitStaggerDuration));

            _fsm.AddState(nameof(EnemyAttackState), new EnemyAttackState(
                EnemyAttackComponent,
                _agentMoveToPlayer,
                _enemyTarget,
                _enemyAnimator,
                 true, false, canExit: (state) => state.Timer.Elapsed >= _enemyConfig.AttackDuration));

            _fsm.AddState(nameof(EnemyIdleState), new EnemyIdleState(
                _enemyAnimator,
                _enemyTarget,
                false, false));

            _fsm.AddState(nameof(EnemyStunnedState), new EnemyStunnedState(
                _tagController, 
                _enemyAnimator,
                true, false
            ));
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}