using CodeBase.Services.Input;
using CodeBase.StateMachine;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public abstract class HeroBaseState : State
    {
        protected HeroStateMachine _heroStateMachine;
        protected IInputService _input;
        protected HeroAnimator _heroAnimator;

        protected HeroBaseState(HeroStateMachine heroStateMachine,
            IInputService input, HeroAnimator heroAnimator)
        {
            _heroStateMachine = heroStateMachine;
            _input = input;
            _heroAnimator = heroAnimator;
            
        }
    }
}
