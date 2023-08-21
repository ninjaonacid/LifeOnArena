using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class ThirdAttackState : HeroBaseAbilityState
    {
        public ThirdAttackState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime : true, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _duration = 0.6f;
            HeroAnimator.PlayAttack(this);
            _heroAttack.BaseAttack();
        }

        public override void OnLogic()
        {
            _duration -= Time.deltaTime;
            if (IsStateOver())
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnExitRequest()
        {
            if (IsStateOver())
            {
                fsm.StateCanExit();
            }
        }

        public override bool IsStateOver() => _duration <= 0;

    }
}
