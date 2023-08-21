using Code.Logic.StateMachine.Base;

namespace Code.Entity.Hero.HeroStates
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
