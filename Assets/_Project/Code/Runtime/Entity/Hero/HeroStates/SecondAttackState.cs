namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class SecondAttackState : HeroBaseAttackState
    {
        public SecondAttackState(HeroAttack heroAttack, HeroWeapon heroWeapon, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroAttack, heroWeapon, heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
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
            _heroAttack.ClearCollisionData();
            _heroWeapon.EnableWeapon(false);
        }
        
    }
}
