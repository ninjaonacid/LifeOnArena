using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class HeroMovementState : HeroBaseState
    {
        private readonly HeroMovement _heroMovement;
        public HeroMovementState(HeroStateMachine heroStateMachine,
            IInputService input,
            HeroAnimator heroAnimator, 
            HeroMovement heroMovement) : base(heroStateMachine, input, heroAnimator)
        {
            _heroMovement = heroMovement;
        }

        public override void Enter()
        {
            HeroAnimator.PlayRun();
            Debug.Log("Entered Movement State");
        }

        public override void Tick(float deltaTime)
        {
            _heroMovement.Movement();
        }

        public override void Exit()
        {
           
        }
    }
}
