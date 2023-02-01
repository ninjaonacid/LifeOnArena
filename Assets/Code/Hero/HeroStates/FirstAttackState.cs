using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseAttackState
    {
        public FirstAttackState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            HeroAnimator.PlayAttack(this);
            HeroAttack.BaseAttack();
            Duration = 0.7f;
            Debug.Log("Entered FirstState");
        }

        public override void OnLogic()
        {
            base.OnLogic();
            Duration -= Time.deltaTime;
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
            if (IsEnded())
            {
                fsm.StateCanExit();
            }
        }
    }
}
