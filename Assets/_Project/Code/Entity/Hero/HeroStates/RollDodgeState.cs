using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class RollDodgeState : HeroBaseAbilityState
    {
        public RollDodgeState(HeroWeapon heroWeapon, HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroWeapon, heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroAnimator.PlayRoll();
            _heroRotation.enabled = false;
            _heroMovement.enabled = false;
            _duration = 1f;
            _heroMovement.ForceMoveTask();
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
            _heroRotation.enabled = true;
            _heroMovement.enabled = true;
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
            _duration <= 0;

    }
}
