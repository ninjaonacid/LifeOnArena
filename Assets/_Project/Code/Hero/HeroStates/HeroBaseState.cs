using Code.Services.Input;
using Code.StateMachine;
using Code.StateMachine.Base;

namespace Code.Hero.HeroStates
{
    public abstract class HeroBaseState : StateBase
    {

        protected HeroAnimator HeroAnimator;


        protected HeroBaseState(HeroAnimator animator, bool needExitTime, bool isGhostState) : base(needExitTime, isGhostState)
        {
            HeroAnimator = animator;
        }
    }
}
