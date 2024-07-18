using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.MeleeEnemy
{
    public class EnemyStunnedState : BaseEnemyState
    {
        private readonly TagController _tagController;

        public EnemyStunnedState(TagController tagController, CharacterAnimator characterAnimator,
            AnimationDataContainer animationData, EnemyTarget enemyTarget, bool needExitTime = false,
            bool isGhostState = false, Action<State<string, string>> onEnter = null,
            Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null,
            Func<State<string, string>, bool> canExit = null) : base(characterAnimator, animationData, enemyTarget,
            needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _tagController = tagController;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            _canExit = (state) => !_tagController.HasTag("Stun");
        }
    }
}