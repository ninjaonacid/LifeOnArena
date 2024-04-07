using Code.Runtime.ConfigData.Animations;
using Code.Runtime.ConfigData.StateMachine;
using Code.Runtime.Entity.Enemy.CommonEnemy;
using Code.Runtime.Modules.StateMachine;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        protected FiniteStateMachine _fsm;

        [SerializeField] protected AgentMoveToPlayer _agentMoveToPlayer;
        [SerializeField] protected EnemyAttackComponent _enemyAttackComponent;
        [SerializeField] protected EnemyAnimator _enemyAnimator;
        [SerializeField] protected EnemyTarget _enemyTarget;
        [SerializeField] protected EnemyHealth _enemyHealth;
        [SerializeField] protected TagController _tagController;
        [SerializeField] protected AnimationDataContainer _animationData;
        [SerializeField] protected StatController _statController;
        [SerializeField] protected EnemyDeath _enemyDeath;


        protected virtual void Start()
        {
            _fsm = new FiniteStateMachine();

            _fsm.AddState(nameof(EnemyChaseState), new EnemyChaseState(
                _agentMoveToPlayer,
                _enemyAnimator,
                _animationData));
            
            _fsm.AddState(nameof(EnemyDeathState), new EnemyDeathState(
                _enemyAnimator,
                _enemyDeath, 
                _animationData,
                _enemyTarget));

            _fsm.AddState(nameof(EnemyStaggerState), new EnemyStaggerState(
                _enemyAnimator,
                _animationData,
                true,
                canExit: (state) => state.Timer.Elapsed >= _statController.Stats["HitRecovery"].Value));

            _fsm.AddState(nameof(EnemyAttackState), new EnemyAttackState(
                _enemyAttackComponent,
                _agentMoveToPlayer,
                _enemyTarget,
                _enemyAnimator,
                _statController,
                _animationData,
                true,
                canExit: (state) => state.Timer.Elapsed >= 
                                    _animationData.Animations[AnimationKey.Attack1].Length * (Mathf.Abs(_enemyAttackComponent.GetAttacksPerSecond()))));

                _fsm.AddState(nameof(EnemyIdleState), new EnemyIdleState(
                _enemyTarget,
                _enemyAnimator,
                _animationData,
                false, false));

            _fsm.AddState(nameof(EnemyStunnedState), new EnemyStunnedState(
                _tagController, 
                _enemyAnimator,
                _animationData,
                true, false
            ));
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}