using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseAttackState
    {
        public FirstAttackState(
            HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
        }

        public override void Enter()
        {
            HeroAnimator.PlayAttack(this);

            Duration = 0.7f;

            Debug.Log("Entered FirstState");
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
