using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public abstract class HeroBaseState : State
    {
        protected readonly CharacterAnimator _characterAnimator;
        protected readonly HeroMovement _heroMovement;
        protected readonly HeroRotation _heroRotation;
        protected readonly AnimationDataContainer _animationData;
        
        protected HeroBaseState(CharacterAnimator characterAnimator, HeroMovement heroMovement, HeroRotation heroRotation, AnimationDataContainer animationData, bool needExitTime = false, bool isGhostState = false, 
            Action<State<string, string>> onEnter = null,
            Action<State<string, string>> onLogic = null, 
            Action<State<string, string>> onExit = null,
            Func<State<string, string>, bool> canExit = null) : base(needExitTime, isGhostState, onEnter, onLogic,
            onExit, canExit)
        {
            _characterAnimator = characterAnimator;
            _heroMovement = heroMovement;
            _heroRotation = heroRotation;
            _animationData = animationData;
        }
    }
}