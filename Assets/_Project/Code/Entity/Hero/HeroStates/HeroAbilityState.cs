using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class HeroAbilityState : HeroBaseState
    {
        
        public HeroAbilityState(HeroAnimator animator, bool needExitTime, bool isGhostState) : base(animator, needExitTime, isGhostState)
        {
        }
    }
}
