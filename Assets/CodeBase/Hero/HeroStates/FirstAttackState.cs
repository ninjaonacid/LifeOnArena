using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseState
    {
        private float duration = 0.5f;
        public FirstAttackState(HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {

        }


        public override void Enter()
        {
            _heroAnimator.PlayAttack(this);
            Debug.Log("Entered FristState");
        }

        public override void Tick(float deltaTime)
        {
           
            duration -= Time.deltaTime;
            if (_input.isAttackButtonUp() && duration > 0)
            {
                _heroStateMachine.ChangeState(new SecondAttackState(_heroStateMachine, _input, _heroAnimator));
            }
            if (duration <= 0)
            {
                _heroStateMachine.ChangeState(new HeroIdleState(_heroStateMachine, _input, _heroAnimator));
            }
        }

        public override void Exit()
        {
          
        }
    }
}
