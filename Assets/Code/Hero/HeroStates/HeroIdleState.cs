using Code.Logic;
using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class HeroIdleState : HeroBaseState
    {
        private int transitionTime = 100;
        public HeroIdleState(HeroStateMachine heroStateMachine,
            IInputService input,
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
        }


        public override void Enter()
        {
            duration = 0.1f;
            HeroAnimator.ToIdleState();
            Debug.Log("IDLE");
        }

        public override void Tick(float deltaTime)
        {
            duration -= deltaTime;
            if (duration <= 0)
            {
                IsEnded = true;
            }
            if (Input.isAttackButtonUp() || Input.isSkillButton1() )
            {
                HeroStateMachine.DoTransition(this);
            }
            if (Input.Axis.sqrMagnitude > Constants.Epsilon)
            {
                HeroStateMachine.Enter<MovementState>();
            }

        }

        public override void Exit()
        {

        }
    }
}
