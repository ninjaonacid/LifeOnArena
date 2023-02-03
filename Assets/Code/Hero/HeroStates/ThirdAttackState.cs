using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class ThirdAttackState : HeroBaseAttackState
    {
        public ThirdAttackState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime : true, isGhostState)
        {
        }


        public override void OnEnter()
        {
            Duration = 0.6f;
            HeroAnimator.PlayAttack(this);
            HeroAttack.BaseAttack();
            Debug.Log("Entered ThirdState");
        }

        public override void OnLogic()
        {
            Duration -= Time.deltaTime;
            if (IsEnded())
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
            if (IsEnded())
            {
                fsm.StateCanExit();
            }
        }
    }
}
