using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class ThirdAbilityState : HeroBaseAbilityState
    {
        public ThirdAbilityState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime : true, isGhostState)
        {
        }


        public override void OnEnter()
        {
            base.OnEnter();
            Duration = 0.6f;
            HeroAnimator.PlayAttack(this);
            HeroAttack.BaseAttack();
          
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
            base.OnExit();
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
