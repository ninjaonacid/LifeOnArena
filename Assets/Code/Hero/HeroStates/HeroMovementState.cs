using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class HeroMovementState : HeroBaseState
    {
        private readonly HeroMovement _heroMovement;


        public HeroMovementState(HeroAnimator animator, HeroMovement heroMovement, bool needExitTime, bool isGhostState) : base(animator, needExitTime, isGhostState)
        {
            _heroMovement = heroMovement;
        }


        public override void OnEnter()
        {
            base.OnEnter();
            HeroAnimator.PlayRun();
        }

        public override void OnLogic()
        {
            _heroMovement.Movement();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
        }
    }
}
