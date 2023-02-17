using Code.Services.Input;
using Code.StateMachine.Base;

namespace Code.Hero.HeroStates
{
    public abstract class HeroBaseAbilityState : HeroBaseState
    {
        protected float Duration;
        protected HeroAttack HeroAttack;

        protected HeroBaseAbilityState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, needExitTime : true, isGhostState)
        {
            HeroAttack = heroAttack;
        }

    }
}
