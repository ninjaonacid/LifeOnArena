using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.Enemy.MeleeEnemy;
using Code.Runtime.Entity.Enemy.RangedEnemy;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Modules.StateMachine;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.Runtime;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        protected FiniteStateMachine _fsm;

        [SerializeField] protected EnemyCastComponent _enemyCastComponent;
        [SerializeField] protected NavMeshMoveToPlayer _navMeshMoveToPlayer;
        [SerializeField] protected EnemyAttackComponent _enemyAttackComponent;
        [SerializeField] protected EnemyWeapon _enemyWeapon;
        [SerializeField] protected CharacterAnimator _characterAnimator;
        [SerializeField] protected EnemyTarget _enemyTarget;
        [SerializeField] protected EnemyHealth _enemyHealth;
        [SerializeField] protected TagController _tagController;
        [SerializeField] protected AnimationDataContainer _animationData;
        [SerializeField] protected StatController _statController;
        [SerializeField] protected EnemyDeath _enemyDeath;
        [SerializeField] protected AbilityController _abilityController;
        [SerializeField] protected EnemyStaggerComponent _enemyStaggerComponent;

        protected virtual void Start()
        {
            _fsm = new FiniteStateMachine();

            _fsm.AddState(nameof(EnemyIdleState), new EnemyIdleState(
                _characterAnimator,
                _animationData,
                _enemyTarget,
                false, false));
            
            _fsm.AddState(nameof(EnemyChaseState), new EnemyChaseState(
                _navMeshMoveToPlayer,
                _characterAnimator,
                _animationData,
                _enemyTarget));

            _fsm.AddState(nameof(EnemyDeathState), new EnemyDeathState(
                _enemyDeath, 
                _characterAnimator,
                _animationData,
                _enemyTarget));

            _fsm.AddState(nameof(EnemyStaggerState), new EnemyStaggerState(
                _characterAnimator,
                _animationData,
                _enemyTarget,
                true,
                canExit: (state) => state.Timer.Elapsed >= (float)_statController.Stats["HitRecovery"].Value));


            _fsm.AddState(nameof(EnemyStunnedState), new EnemyStunnedState(
                _tagController, 
                _characterAnimator,
                _animationData,
                _enemyTarget,
                true, false
            ));
            
           
        }

        private void Update()
        {
            _fsm.OnLogic();
        }
    }
}