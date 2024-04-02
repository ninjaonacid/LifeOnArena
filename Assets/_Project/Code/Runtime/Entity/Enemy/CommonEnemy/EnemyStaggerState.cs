using System;
using Code.Runtime.Logic.Timer;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyStaggerState : CommonEnemyState
    {
        public EnemyStaggerState(EnemyAnimator enemyAnimator, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            enemyAnimator, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
        }


        public override void OnEnter()
        {
            base.OnEnter();
            _enemyAnimator.PlayHit();
        }

        public override void OnLogic()
        {
            base.OnLogic();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
        }

        public override bool IsStateOver()
        {
            return base.IsStateOver();
        }
    }
}