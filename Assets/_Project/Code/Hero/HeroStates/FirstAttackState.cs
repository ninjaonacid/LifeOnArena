using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseAbilityState
    {
        public FirstAttackState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            HeroAnimator.PlayAttack(this);
            _heroAttack.BaseAttack();
            _duration = 0.6f;
        }

        public override void OnLogic()
        {
            base.OnLogic();
            _duration -= Time.deltaTime;

            if(IsStateOver()) fsm.StateCanExit(); 

        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();

            if (IsStateOver())
            {
                fsm.StateCanExit();
            }
        }

        public override bool IsStateOver() => _duration <= 0;



    }
}
