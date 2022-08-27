using CodeBase.Logic;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public class HeroIdleState : HeroBaseState
    {
        
        private HeroMovement _heroMovement;

        public HeroIdleState(HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator, 
            HeroMovement heroMovement) : base(heroStateMachine, input, heroAnimator)
        {
            _heroMovement = heroMovement;
        }


        public override void Enter()
        {
           _heroAnimator.ToIdleState();
        }

        public override void Tick(float deltaTime)
        {
           

            if (_input.isAttackButtonUp())
            {
                _heroStateMachine.Enter<FirstAttackState>();

            }

            
            if (_input.Axis.sqrMagnitude > Constants.Epsilon)
            {
                _heroStateMachine.Enter<MovementState>();
            }

        }

        public override void Exit()
        {
            
        }
    }
}
