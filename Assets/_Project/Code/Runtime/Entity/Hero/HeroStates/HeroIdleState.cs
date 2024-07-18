using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class HeroIdleState : HeroBaseState
    {
        public HeroIdleState(CharacterAnimator characterAnimator, HeroMovement heroMovement, HeroRotation heroRotation,
            AnimationDataContainer animationData, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            characterAnimator, heroMovement, heroRotation, animationData, needExitTime, isGhostState, onEnter, onLogic,
            onExit, canExit)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _characterAnimator.PlayAnimation(_animationData.Animations[AnimationKey.Idle].Hash);
        }

        public override void OnLogic()
        {
            base.OnLogic();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}