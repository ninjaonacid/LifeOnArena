namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseAttackState
    {
        public FirstAttackState(HeroAttackComponent heroAttackComponent, HeroWeapon heroWeapon, HeroAnimator heroAnimator,
            HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(
            heroAttackComponent, heroWeapon, heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
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
            HeroAttackComponent.ClearCollisionData();
            _heroWeapon.EnableWeapon(false);
        }
    }
}