using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class SecondAttackState : HeroBaseAttackState
    {
        public SecondAttackState(
            HeroStateMachine heroStateMachine,
            IInputService input,
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
        }

        public override void Enter()
        {
            HeroAnimator.PlayAttack(this);
            IsEnded = false;
            Duration = 0.7f;
            Debug.Log("Entered SecondState");
        }

        public override void Tick(float deltaTime)
        {
            Duration -= deltaTime;

            if (TransitionCondition())
            {
                if(!HeroStateMachine.IsInTransition)
                    HeroStateMachine.DoTransition(this);
            }

            if (Duration <= 0)
            {
                IsEnded = true;
            }
        }

        private bool TransitionCondition()
        {
            return Input.isAttackButtonUp() || Input.isSkillButton1() || 
                   Input.isSkillButton2() || Input.isSkillButton3() || Duration <= 0;
        }

        public override void Exit()
        {

        }



    }
}
