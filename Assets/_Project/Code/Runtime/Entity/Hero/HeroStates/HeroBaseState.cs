using System;
using Code.Runtime.Modules.StateMachine.Base;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public abstract class HeroBaseState : State
    {
        protected readonly HeroAnimator _heroAnimator;
        protected readonly HeroMovement _heroMovement;
        protected readonly HeroRotation _heroRotation;
        
        protected HeroBaseState(HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation,
            bool needExitTime = false, bool isGhostState = false, 
            Action<State<string, string>> onEnter = null,
            Action<State<string, string>> onLogic = null, 
            Action<State<string, string>> onExit = null,
            Func<State<string, string>, bool> canExit = null) : base(needExitTime, isGhostState, onEnter, onLogic,
            onExit, canExit)
        {
            _heroAnimator = heroAnimator;
            _heroMovement = heroMovement;
            _heroRotation = heroRotation;
            
        }
    }
}