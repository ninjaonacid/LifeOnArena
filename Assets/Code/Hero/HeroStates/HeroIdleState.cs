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
        }

        public override void Tick(float deltaTime)
        {

            if (Input.isAttackButtonUp())
            {
                HeroStateMachine.Enter<FirstAttackState>();
            }

            if (Input.isSkillButton1())
            {
                HeroStateMachine.Enter<SpinAttackState>();
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
