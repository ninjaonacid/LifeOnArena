using System;
using Code.Services.Input;
using UnityEngine;
using UniRx;
namespace Code.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseState
    {
        private bool _isInTransition;
        public FirstAttackState(
            HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
        }

        public override void Enter()
        {
            HeroAnimator.PlayAttack(this);
            duration = 0.7f;
            TransitionTime = 1000;
            Debug.Log("Entered FirstState");
            IsEnded = false;
            _isInTransition = false;
        }

        public override void Tick(float deltaTime)
        {

            duration -= deltaTime;
            if (duration <= 0)
            {
                IsEnded = true;
            }
            
            if (Input.isAttackButtonUp() || Input.isSkillButton1() || duration <= 0f)
            {
                if(!_isInTransition)
                    HeroStateMachine.DoTransition(this);

                _isInTransition = true;
            }

            
            /*else if (Input.isSkillButton1())
            {
                HeroStateMachine.GetTransition(this, TransitionTime);
            }

            if (duration <= 0f)
            {
                HeroStateMachine.GetTransition(this, TransitionTime);
            }*/
            /*if (_canChain)
            {
                _attackTransition -= deltaTime;
                if (_attackTransition <= 0)
                {
                    HeroStateMachine.Enter<SecondAttackState>();
                }
            }*/
     
        }

        public override void Exit()
        {
        
        }

        public bool StateEnds()
        {
            return duration <= 0;
        }
    }
}
