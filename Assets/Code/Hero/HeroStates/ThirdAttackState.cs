using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class ThirdAttackState : HeroBaseAttackState
    {

        public ThirdAttackState(
            HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
        }

        public override void Enter()
        {
            Duration = 0.7f;
            IsEnded = false;
            IsInTransition = false;
            HeroAnimator.PlayAttack(this);
            Debug.Log("Entered ThirdState");
        }

        public override void Tick(float deltaTime)
        {
            Duration -= deltaTime;
            
            if (StateEnds())
            {
                IsEnded = true;
                HeroStateMachine.Enter<HeroIdleState>();
            }
        }

        private bool StateEnds()
        {
            return Duration < 0;
        }

        public override void Exit()
        {
            

        }
    }
}
