using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class SpinAttackState : HeroBaseAttackState
    {
        private readonly HeroRotation _heroRotation;


        public SpinAttackState(HeroAnimator animator, HeroAttack heroAttack, HeroRotation heroRotation, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime : true, isGhostState)
        {
            _heroRotation = heroRotation;
        }


        public override void OnEnter()
        {
            base.OnEnter();
            _heroRotation.enabled = false;
            HeroAnimator.PlayAttack(this);
            HeroAttack.SkillAttack();
            Debug.Log("Enter spinAttack");
            Duration = 0.7f;
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
            _heroRotation.enabled = true;
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
