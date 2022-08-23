using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public class HeroIdleState : HeroBaseState
    {

        public HeroIdleState(HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
  
        }


        public override void Enter()
        {
           Debug.Log("Enter idle");
        }

        public override void Tick(float deltaTime)
        {
            if (_input.isAttackButtonUp())
            {
                _heroStateMachine.ChangeState(new FirstAttackState(_heroStateMachine, _input, _heroAnimator));
            }

        }

        public override void Exit()
        {
            
        }
    }
}
