using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.MeleeEnemy
{
    public class EnemyDeathState : BaseEnemyState
    {
        private EnemyDeath _enemyDeath;

        public EnemyDeathState(EnemyDeath enemyDeath, CharacterAnimator characterAnimator,
            AnimationDataContainer animationData, EnemyTarget enemyTarget, bool needExitTime = false,
            bool isGhostState = false, Action<State<string, string>> onEnter = null,
            Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null,
            Func<State<string, string>, bool> canExit = null) : base(characterAnimator, animationData, enemyTarget,
            needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _enemyDeath = enemyDeath;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _enemyTarget.enabled = false;
            //_characterAnimator.PlayDie();
        }

        public override void OnExit()
        {
            base.OnExit();
            _enemyTarget.enabled = true;
        }
    }
}