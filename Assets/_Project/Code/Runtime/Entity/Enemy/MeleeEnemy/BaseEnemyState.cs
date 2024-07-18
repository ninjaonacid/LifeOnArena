using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.MeleeEnemy
{
    public class BaseEnemyState : State
    {
        protected readonly CharacterAnimator _characterAnimator;
        protected readonly AnimationDataContainer _animationData;
        protected readonly EnemyTarget _enemyTarget;

        public BaseEnemyState(CharacterAnimator characterAnimator, AnimationDataContainer animationData, EnemyTarget enemyTarget, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _characterAnimator = characterAnimator;
            _animationData = animationData;
            _enemyTarget = enemyTarget;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            //Debug.Log($"Entered {this.name}");
        }

        public override void OnLogic()
        {
            base.OnLogic();
        }

        public override void OnExit()
        {
            base.OnExit();
            //Debug.Log($"Exited {this.name}");
        }
    }
}