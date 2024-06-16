using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.MeleeEnemy
{
    public class BaseEnemyState : State
    {
        protected readonly EnemyAnimator _enemyAnimator;
        protected readonly AnimationDataContainer _animationData;

        public BaseEnemyState(EnemyAnimator enemyAnimator, AnimationDataContainer animationData, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _enemyAnimator = enemyAnimator;
            _animationData = animationData;
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