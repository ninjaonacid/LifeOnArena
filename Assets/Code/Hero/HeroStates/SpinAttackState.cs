using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class SpinAttackState : HeroBaseState
    {
        public SpinAttackState(HeroStateMachine heroStateMachine, IInputService input, HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
        }

        public override void Enter()
        {
            HeroAnimator.PlaySpinAttackSkill();
            duration = 1f;
            attackTransition = 0.5f;
        }

        public override void Tick(float deltaTime)
        {
            duration -= deltaTime;
            if (duration <= 0f)
            {
                HeroStateMachine.Enter<HeroIdleState>();
            }
        }

        public override void Exit()
        {
        }
    }
}
