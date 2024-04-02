using UnityEngine;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class SpinAbilityState : HeroBaseAbilityState
    {
        public SpinAbilityState(HeroWeapon heroWeapon, HeroSkills heroSkills, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroWeapon, heroSkills, heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroRotation.enabled = false;
            _heroWeapon.EnableWeapon(true);
            _heroAnimator.PlaySpinAttackSkill();
        }
        
        public override void OnExit()
        {
            _heroRotation.enabled = true;
            _heroWeapon.EnableWeapon(false);
        }

     
        
    }
}
