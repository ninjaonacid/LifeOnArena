using System;
using Code.Services.Input;
using UnityEngine;
using UniRx;
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
            IsEnded = false;
            IsInTransition = false;
        }

        public override void Tick(float deltaTime)
        {
            Duration -= deltaTime;
            
            if (Input.isAttackButtonUp() || Input.isSkillButton1() || Duration <= 0)
            {
                if(!IsInTransition)
                    HeroStateMachine.DoTransition(this);

                IsInTransition = true;
            }

            if (Duration <= 0)
            {
                IsEnded = true;
            }
        }

        public override void Exit()
        {
        
        }

        public bool StateEnds()
        {
            return Duration <= 0;
        }
    }
}
