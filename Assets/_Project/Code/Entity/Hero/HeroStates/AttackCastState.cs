using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class AttackCastState : HeroBaseAttackState
    {
        public AttackCastState(HeroAnimator animator, HeroAttack heroAttack, HeroWeapon heroWeapon, bool needExitTime, bool isGhostState) : base(animator, heroAttack, heroWeapon, needExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _heroAttack.SkillAttack();
        }
    }
}
