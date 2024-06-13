using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.Enemy.MeleeEnemy;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.RangedEnemy
{
    public class RangedEnemyAttackState : BaseEnemyState
    {
        private AbilityController _abilityController;
        private AgentMoveToPlayer _moveToPlayer;
        private MeleeEnemyAttackComponent _meleeEnemyAttack;
        private EnemyTarget _enemyTarget;
        
        public RangedEnemyAttackState(EnemyAnimator enemyAnimator, AnimationDataContainer animationData,
            AbilityController abilityController, AgentMoveToPlayer moveToPlayer,
            MeleeEnemyAttackComponent meleeEnemyAttack, EnemyTarget enemyTarget,
            bool needExitTime = false, bool isGhostState = false, Action<State<string, string>> onEnter = null,
            Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null,
            Func<State<string, string>, bool> canExit = null) : base(enemyAnimator, animationData, needExitTime,
            isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _abilityController = abilityController;
            _moveToPlayer = moveToPlayer;
            _meleeEnemyAttack = meleeEnemyAttack;
            _enemyTarget = enemyTarget;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _enemyAnimator.PlayAnimation(_animationData.Animations[AnimationKey.SpellCast].Hash);
            
        }

        public override void OnLogic()
        {
            base.OnLogic();
            
            _enemyTarget.RotationToTarget();
            
            if(Timer.Elapsed >= _animationData.Animations[AnimationKey.SpellCast].Length - 0.2f)
            {
                _abilityController.TryActivateAbility("FireballId");
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _meleeEnemyAttack.AttackEnded();

        }
    }
}