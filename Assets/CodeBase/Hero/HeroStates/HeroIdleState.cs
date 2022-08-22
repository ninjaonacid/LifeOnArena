using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public class HeroIdleState : HeroBaseState
    {
        private readonly HeroStateMachine _heroStateMachine;
        private readonly IInputService _inputService;
        private readonly HeroAnimator _heroAnimator;

        public HeroIdleState(HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
            _heroStateMachine = heroStateMachine;
            _inputService = input;
            _heroAnimator = heroAnimator;
        }


        public override void Enter()
        {
           Debug.Log("Enter idle");
        }

        public override void Tick(float deltaTime)
        {
            if (_inputService.isAttackButtonUp())
            {
                _heroStateMachine.ChangeState(new FirstAttackState(_heroStateMachine, _inputService, _heroAnimator));
            }
        }

        public override void Exit()
        {
            
        }
    }
}
