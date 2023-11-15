using Code.Logic.StateMachine.Base;

namespace Code.Entity.Hero.HeroStates
{
    public abstract class HeroBaseState : StateBase
    {
        protected readonly HeroAnimator _heroAnimator;
        protected readonly HeroMovement _heroMovement;
        protected readonly HeroRotation _heroRotation;
        protected HeroBaseState(HeroAnimator animator, bool needExitTime, bool isGhostState) : base(needExitTime, isGhostState)
        {
            _heroAnimator = animator;
        }
    }
}
