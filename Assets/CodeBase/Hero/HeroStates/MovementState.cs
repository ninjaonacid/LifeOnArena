using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public class MovementState : HeroBaseState
    {
        private readonly HeroMovement _heroMovement;
        public MovementState(HeroStateMachine heroStateMachine,
            IInputService input,
            HeroAnimator heroAnimator, 
            HeroMovement heroMovement) : base(heroStateMachine, input, heroAnimator)
        {
            _heroMovement = heroMovement;
        }

        public override void Enter()
        {
            _heroAnimator.PlayRun();
        }

        public override void Tick(float deltaTime)
        {
            _heroMovement.Movement();

            if (_heroMovement.GetVelocity() <= 0)
            {
                _heroStateMachine.Enter<HeroIdleState>();
            } 
            else if (_input.isAttackButtonUp())
            {
                _heroStateMachine.Enter<FirstAttackState>();
            }

        }

        public override void Exit()
        {
           _heroMovement.StopMove();
        }
    }
}
