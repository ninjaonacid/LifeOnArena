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
            HeroAnimator.PlayAttack(this);
            Debug.Log("Entered ThirdState");
        }

        public override void Tick(float deltaTime)
        {
            Duration -= deltaTime;

            if (Input.isAttackOrSkillPressed() || IsEnded() && !HeroStateMachine.IsInTransition)
            {
                HeroStateMachine.DoTransition(this);
            }
        }

        public override void Exit()
        {
        }
    }
}
