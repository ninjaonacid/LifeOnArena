using Code.Runtime.Modules.StateMachine.Base;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public abstract class HeroBaseState : StateBase
    {
        protected readonly HeroAnimator _heroAnimator;
        protected readonly HeroMovement _heroMovement;
        protected readonly HeroRotation _heroRotation;
       
        protected HeroBaseState(HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
        {
            _heroAnimator = heroAnimator;
            _heroMovement = heroMovement;
            _heroRotation = heroRotation;
        }
    }
}
