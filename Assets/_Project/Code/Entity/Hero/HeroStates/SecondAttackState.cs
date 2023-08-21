using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class SecondAttackState : HeroBaseAbilityState
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
            _heroAttack.BaseAttack();
            _duration = 0.8f;
        }

        public override void OnLogic()
        {
            base.OnLogic();
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
