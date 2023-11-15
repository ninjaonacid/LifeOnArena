using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class AbilityCastState : HeroBaseAbilityState
    {
        public AbilityCastState(HeroWeapon heroWeapon, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroWeapon, heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
        }
    }
}
