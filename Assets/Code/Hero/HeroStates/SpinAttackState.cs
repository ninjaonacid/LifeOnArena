using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class SpinAttackState : HeroBaseAttackState
    {
        private readonly HeroRotation _heroRotation;


        public SpinAttackState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime : true, isGhostState)
        {
        }


        public override void OnEnter()
        {
            _heroRotation.enabled = false;
            HeroAnimator.PlayAttack(this);
            HeroAttack.SkillAttack();
            Debug.Log("Enter spinAttack");
            Duration = 1f;
        }

        public override void OnLogic()
        {
            Duration -= Time.deltaTime;
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
