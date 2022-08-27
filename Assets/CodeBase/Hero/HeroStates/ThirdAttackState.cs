using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public class ThirdAttackState : HeroBaseState
    {
        private float _duration = 1f;

        public ThirdAttackState(
            HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
        }

        public override void Enter()
        {
            _heroAnimator.PlayAttack(this);
            Debug.Log("Entered ThirdState");
        }

        public override void Tick(float deltaTime)
        {
            _duration -= deltaTime;
            
            if (StateEnds())
            {
                _heroStateMachine.Enter<HeroIdleState>();
            }
        }

        private bool StateEnds()
        {
            return _duration < 0;
        }

        public override void Exit()
        {
            _duration = 0.7f;

        }
    }
}
