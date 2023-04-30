using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class SpinAbilityState : HeroBaseAbilityState
    {
        private readonly HeroRotation _heroRotation;
        
        public SpinAbilityState(
            HeroAnimator animator, 
            HeroAttack heroAttack, 
            HeroRotation heroRotation, 
            bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime : true, isGhostState)
        {
            _heroRotation = heroRotation;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroRotation.enabled = false;
            HeroAnimator.PlayAttack(this);
            HeroAttack.SkillAttack();
            Duration = 0.7f;
        }

        public override void OnLogic()
        {
            Duration -= Time.deltaTime;
            if (IsStateOver())
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            _heroRotation.enabled = true;
        }

        public override void OnExitRequest()
        {
            if (IsStateOver())
            {
                fsm.StateCanExit();
            }
        }

        public override bool IsStateOver() => Duration <= 0;

    }
}
