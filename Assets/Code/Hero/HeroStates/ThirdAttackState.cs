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

            if (Input.isAttackButtonUp() || Input.isSkillButton1() || Input.isSkillButton2() ||
                Input.isSkillButton3() || Duration <= 0)
            {
                if (!IsInTransition)
                {
                    HeroStateMachine.DoTransition(this);
                    IsInTransition = true;
                }
            }
            if (StateEnds())
            {
                IsEnded = true;
                HeroStateMachine.DoTransition(this);
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
