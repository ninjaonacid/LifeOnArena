using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class SecondAttackState : HeroBaseAttackState
    {
        public SecondAttackState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime:true, isGhostState)
        {

        }

        public override void Init()
        {
            base.Init();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            HeroAnimator.PlayAttack(this);
            HeroAttack.BaseAttack();
            Duration = 0.6f;
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
            if (IsEnded())
            {
                fsm.StateCanExit();
            }
        }
    }
}
