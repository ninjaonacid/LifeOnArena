using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class AbilityCastState : HeroBaseAbilityState
    {
        public AbilityCastState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _heroAttack.SkillAttack();
        }
    }
}
