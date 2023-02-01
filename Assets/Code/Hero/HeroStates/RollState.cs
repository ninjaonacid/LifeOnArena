using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class RollState : HeroBaseAttackState
    {
        private readonly HeroMovement _heroMovement;
        private readonly HeroRotation _heroRotation;

        public RollState(HeroAnimator animator, HeroAttack heroAttack, bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime, isGhostState)
        {
        }
        public override void OnEnter()
        {
            base.OnEnter();
            HeroAnimator.PlayRoll();
            _heroRotation.enabled = false;

            Duration = 1f;
        }

        public override void OnLogic()
        {
            base.OnLogic();
            Duration -= Time.deltaTime;
            _heroMovement.ForceMove();
        }

        public override void OnExit()
        {
            base.OnExit();
            _heroRotation.enabled = true;
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
        }
    }
}
