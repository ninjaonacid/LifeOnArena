using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class AbilityCastState : HeroBaseAbilityState
    {
        public AbilityCastState(HeroWeapon heroWeapon, HeroSkills heroSkills, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroWeapon, heroSkills, heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
        }
    }
}
