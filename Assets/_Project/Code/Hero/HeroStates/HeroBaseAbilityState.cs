namespace Code.Hero.HeroStates
{
    public abstract class HeroBaseAbilityState : HeroBaseState
    {
        protected float _duration;
        protected HeroAttack _heroAttack;

        protected HeroBaseAbilityState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, needExitTime : true, isGhostState)
        {
            _heroAttack = heroAttack;
        }
    }
}
