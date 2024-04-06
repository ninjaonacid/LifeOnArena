using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyDeathState : CommonEnemyState
    {
        private EnemyDeath _enemyDeath;
        private EnemyTarget _enemyTarget;
        public EnemyDeathState(EnemyAnimator enemyAnimator, EnemyDeath enemyDeath, AnimationDataContainer animationData,
            EnemyTarget enemyTarget, bool needExitTime = false, bool isGhostState = false, Action<State<string, string>> onEnter = null,
            Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null,
            Func<State<string, string>, bool> canExit = null) : base(enemyAnimator, animationData, needExitTime,
            isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _enemyDeath = enemyDeath;
            _enemyTarget = enemyTarget;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _enemyTarget.enabled = false;
        }

        public override void OnExit()
        {
            base.OnExit();
            _enemyTarget.enabled = true;
        }
    }
}