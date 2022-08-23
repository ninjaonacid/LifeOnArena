using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public class MovementState : HeroBaseState
    {
        private HeroStateMachine _heroStateMachine;
        private IInputService _inputService;
        private HeroAnimator _heroAnimator;
        public MovementState(HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
            _heroStateMachine = heroStateMachine;
            _inputService = input;
            _heroAnimator = heroAnimator;
            
        }

        public override void Enter()
        {
        }

        public override void Tick(float deltaTime)
        {
            _heroStateMachine.heroMovement.Movement();

        }

        public override void Exit()
        {
        }
    }
}
