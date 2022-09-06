using System;
using Code.Services.Input;
using UnityEngine;
using UniRx;
namespace Code.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseState
    {
        private float _duration;
        private float _attackTransition;
        private bool _canChain;
        public FirstAttackState(
            HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
 
        }

        public override void Enter()
        {
            HeroAnimator.PlayAttack(this);
            _duration = 1f;
            _attackTransition = 0.5f;
            _canChain = false;
            Func<bool> AttackPressed() => () => Input.isAttackButtonUp();
            Func<bool> FirstSkillPressed() => () => Input.isSkillButton1();
            HeroStateMachine.AddTransition(this, HeroStateMachine.GetState<SecondAttackState>(), attackTransition, AttackPressed());
            HeroStateMachine.AddTransition(this, HeroStateMachine.GetState<SpinAttackState>(), attackTransition, FirstSkillPressed());
            Debug.Log("Entered FirstState");

        }

        public override void Tick(float deltaTime)
        {


            HeroStateMachine.MakeTransition(this);
            /*_duration -= deltaTime;
            if (Input.isAttackButtonUp())
            {
                _canChain = true;
                HeroStateMachine.MakeTransition(this);
            }

            if (Input.isSkillButton1())
            {
                HeroStateMachine.Enter<SpinAttackState>();
            }

            if (_canChain)
            {
                _attackTransition -= deltaTime;
                if (_attackTransition <= 0)
                {
                    HeroStateMachine.Enter<SecondAttackState>();
                }
            }
            if (StateEnds())
            {
                HeroStateMachine.Enter<HeroIdleState>();
            }*/
        }

        public override void Exit()
        {

        }

        private bool StateEnds()
        {
            return _duration <= 0;
        }
    }
}
