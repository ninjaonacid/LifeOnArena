using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class SpinAbilityState : HeroBaseAbilityState
    {
        private readonly HeroRotation _heroRotation;
        public SpinAbilityState(
            HeroAnimator animator, 
            HeroAttack heroAttack, 
            HeroRotation heroRotation, 
            bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime : true, isGhostState)
        {
            _heroRotation = heroRotation;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroRotation.enabled = false;
            HeroAnimator.PlayAttack(this);
            _heroAttack.SetCollisionOn();
            _duration = 0.7f;
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
            _heroRotation.enabled = true;
            _heroAttack.SetCollisionOff();
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
