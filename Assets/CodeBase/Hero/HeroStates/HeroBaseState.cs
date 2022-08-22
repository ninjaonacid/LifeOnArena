using CodeBase.Services.Input;
using CodeBase.StateMachine;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public abstract class HeroBaseState : State
    {
        private HeroStateMachine _heroStateMachine;
        private IInputService _inputService;
        private HeroAnimator _heroAnimator;
        protected HeroBaseState(HeroStateMachine heroStateMachine,
            IInputService input, HeroAnimator heroAnimator)
        {
            _heroStateMachine = heroStateMachine;
            _inputService = input;
            _heroAnimator = heroAnimator;
        }
    }
}
