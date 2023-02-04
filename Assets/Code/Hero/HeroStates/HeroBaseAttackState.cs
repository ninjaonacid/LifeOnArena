using Code.Services.Input;
using Code.StateMachine.Base;

namespace Code.Hero.HeroStates
{
    public abstract class HeroBaseAttackState : HeroBaseState
    {
        protected float Duration;
        protected HeroAttack HeroAttack;
        
        public bool IsEnded() => Duration <= 0;

        protected HeroBaseAttackState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, needExitTime : true, isGhostState)
        {
            HeroAttack = heroAttack;
        }

    }
}
