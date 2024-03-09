namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class SecondAttackState : HeroBaseAttackState
    {
        public SecondAttackState(HeroAttackComponent heroAttackComponent, HeroWeapon heroWeapon, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroAttackComponent, heroWeapon, heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            _heroAnimator.PlayAttack(this);
            _heroWeapon.EnableWeapon(true);
            _duration = _heroWeapon.GetEquippedWeaponData().WeaponFsmConfig.SecondAttackStateDuration;
        }
        
        public override void OnExit()
        {
            base.OnExit();
            HeroAttackComponent.ClearCollisionData();
            _heroWeapon.EnableWeapon(false);
        }
        
    }
}
