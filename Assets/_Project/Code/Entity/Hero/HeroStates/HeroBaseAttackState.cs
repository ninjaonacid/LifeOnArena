namespace Code.Entity.Hero.HeroStates
{
    public abstract class HeroBaseAttackState : HeroBaseState
    {
        protected readonly HeroAttack _heroAttack;
        protected readonly HeroWeapon _heroWeapon;
        protected float _duration;


        protected HeroBaseAttackState(HeroAttack heroAttack, HeroWeapon heroWeapon, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
            _heroAttack = heroAttack;
            _heroWeapon = heroWeapon;
        }
    }
}
