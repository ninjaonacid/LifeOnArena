using UnityEngine;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public class RollDodgeState : HeroBaseAbilityState
    {
        public RollDodgeState(HeroWeapon heroWeapon, HeroSkills heroSkills, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) 
            : base(heroWeapon, heroSkills, heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroAnimator.PlayRoll();
            _heroRotation.enabled = false;
        }

        public override void OnExit()
        {
            base.OnExit();
            _heroRotation.enabled = true;
        }

      
    }
}
