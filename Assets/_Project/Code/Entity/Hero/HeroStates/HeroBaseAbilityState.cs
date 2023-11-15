using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public abstract class HeroBaseAbilityState : HeroBaseState
    {
        protected readonly HeroWeapon _heroWeapon;
        protected float _duration;

        protected HeroBaseAbilityState(HeroWeapon heroWeapon, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
            _heroWeapon = heroWeapon;
        }
    }
}
