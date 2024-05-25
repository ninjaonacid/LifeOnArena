using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.Enemy.MeleeEnemy;
using Code.Runtime.Logic.Timer;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyStaggerState : BaseEnemyState
    {
        public EnemyStaggerState(EnemyAnimator enemyAnimator, AnimationDataContainer animationData, bool needExitTime = false, bool isGhostState = false, Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(enemyAnimator, animationData, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _enemyAnimator.PlayAnimation(_animationData.Animations[AnimationKey.Hit].Hash);
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
        
    }
}