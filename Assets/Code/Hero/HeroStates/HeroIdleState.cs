using Code.Logic;
using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
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
            HeroAnimator.ToIdleState();
            Debug.Log("IDLE");
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void Exit()
        {

        }
    }
}
