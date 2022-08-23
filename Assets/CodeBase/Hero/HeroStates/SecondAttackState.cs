using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public class SecondAttackState : HeroBaseState
    {
        private float duration = 0.5f;
        public SecondAttackState(HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {

        }

        public override void Enter()
        {
            _heroAnimator.PlayAttack(this);
            Debug.Log("Entered secondState");
        }

        public override void Tick(float deltaTime)
        {
            duration -= Time.deltaTime;
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
