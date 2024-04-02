using UnityEngine;

namespace Code.Runtime.Entity.Hero.HeroStates
{
    public abstract class HeroBaseAbilityState : HeroBaseState
    {
        protected readonly HeroWeapon _heroWeapon;
        protected readonly HeroSkills _heroSkills;
        protected float _duration;
        protected HeroBaseAbilityState(HeroWeapon heroWeapon, HeroSkills heroSkills, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
            _heroWeapon = heroWeapon;
            _heroSkills = heroSkills;
        }
        
    }
}
