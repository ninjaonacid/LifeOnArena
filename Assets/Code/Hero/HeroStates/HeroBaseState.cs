using Code.Services.Input;
using Code.StateMachine;

namespace Code.Hero.HeroStates
{
    public abstract class HeroBaseState : State
    {

        protected float duration;
        protected float attackTransition;
        protected HeroStateMachine HeroStateMachine;
        protected IInputService Input;
        protected HeroAnimator HeroAnimator;

        protected HeroBaseState(HeroStateMachine heroStateMachine,
            IInputService input, HeroAnimator heroAnimator)
        {
            HeroStateMachine = heroStateMachine;
            Input = input;
            HeroAnimator = heroAnimator;
            
        }
    }
}
