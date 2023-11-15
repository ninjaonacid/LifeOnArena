namespace Code.Entity.Hero.HeroStates
{
    public abstract class HeroBaseAttackState : HeroBaseState
    {
        protected readonly HeroAttack _heroAttack;
        protected readonly HeroWeapon _heroWeapon;
        protected float _duration;
        protected HeroBaseAttackState(HeroAnimator animator, HeroAttack heroAttack, HeroWeapon heroWeapon, bool needExitTime, bool isGhostState) : base(animator, needExitTime : true, isGhostState)
        {
            _heroAttack = heroAttack;
            _heroWeapon = heroWeapon;
        }
    }
}
