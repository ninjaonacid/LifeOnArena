namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseAttackState
    {
        public FirstAttackState(HeroAttack heroAttack, HeroWeapon heroWeapon, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroAttack, heroWeapon, heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroWeapon.EnableWeapon(true);
            _heroAnimator.PlayAttack(this);
            _duration = _heroWeapon.GetEquippedWeaponData().WeaponFsmConfig.FirstAttackStateDuration;
        }
        

        public override void OnExit()
        {
            base.OnExit();
            _heroAttack.ClearCollisionData();
            _heroWeapon.EnableWeapon(false);
        }

    }
}