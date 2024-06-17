using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class HeroStompAbilityState : HeroBaseAbilityState
    {
        public HeroStompAbilityState(HeroWeapon heroWeapon, HeroAbilityController heroAbilityController,
            HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation,
            AnimationDataContainer animationData, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            heroWeapon, heroAbilityController, heroAnimator, heroMovement, heroRotation, animationData, needExitTime,
            isGhostState, onEnter, onLogic, onExit, canExit)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _heroAnimator.PlayAnimation(_animationData.Animations[AnimationKey.Stomp].Hash);
        }

        public override void OnLogic()
        {
            base.OnLogic();
            
            if(Timer.Elapsed >= _animationData.Animations[AnimationKey.Stomp].Length)
            {
                fsm.StateCanExit();
            }
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