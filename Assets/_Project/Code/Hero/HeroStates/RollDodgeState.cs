using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class RollDodgeState : HeroBaseAbilityState
    {
        private readonly HeroMovement _heroMovement;
        private readonly HeroRotation _heroRotation;
        public RollDodgeState(HeroAnimator animator, 
            HeroAttack heroAttack,
            HeroMovement heroMovement,
            HeroRotation heroRotation,
            bool needExitTime, bool isGhostState) : base(animator, heroAttack, needExitTime, isGhostState)
        {
            _heroMovement = heroMovement;
            _heroRotation = heroRotation;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            HeroAnimator.PlayRoll();
            //_heroRotation.enabled = false;

            Duration = 1f;
        }

        public override void OnLogic()
        {
            base.OnLogic();
            Duration -= Time.deltaTime;
            _heroMovement.ForceMove();
            if (IsStateOver())
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _heroRotation.enabled = true;
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
            if (IsStateOver())
            {
                fsm.StateCanExit();
            }
        }

        public override bool IsStateOver() =>
            Duration <= 0;

    }
}
