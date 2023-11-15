using UnityEngine;

namespace Code.Entity.Hero.HeroStates
{
    public class RollDodgeState : HeroBaseAttackState
    {
        private readonly HeroMovement _heroMovement;
        private readonly HeroRotation _heroRotation;

        public RollDodgeState(HeroAnimator animator, HeroAttack heroAttack, HeroWeapon heroWeapon, HeroMovement heroMovement, HeroRotation heroRotation, bool needExitTime, bool isGhostState) : base(animator, heroAttack, heroWeapon, needExitTime, isGhostState)
        {
            _heroMovement = heroMovement;
            _heroRotation = heroRotation;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroAnimator.PlayRoll();
            //_heroRotation.enabled = false;
            _duration = 1f;
        }

        public override void OnLogic()
        {
            base.OnLogic();
            _duration -= Time.deltaTime;
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
            _duration <= 0;

    }
}
