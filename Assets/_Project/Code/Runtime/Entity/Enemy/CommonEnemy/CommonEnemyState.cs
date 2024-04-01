using System;
using Code.Runtime.Logic.Timer;
using Code.Runtime.Modules.StateMachine.Base;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class CommonEnemyState : State
    {
        protected readonly EnemyAnimator _enemyAnimator;
        
        public CommonEnemyState(EnemyAnimator enemyAnimator, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _enemyAnimator = enemyAnimator;
        }
    }
}