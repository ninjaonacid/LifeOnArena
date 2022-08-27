using System.Numerics;
using CodeBase.Logic;
using CodeBase.Services.Input;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


namespace CodeBase.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseState
    {
        private float _duration;
        private float _attackTransition;
        private bool _canChain;
        private HeroLookRotation _heroLook;
        public FirstAttackState(
            HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator, HeroLookRotation heroLook) : base(heroStateMachine, input, heroAnimator)
        {
            _heroLook = heroLook;
        }

        public override void Enter()
        {
            //_heroLook.OnAttackEnterRotation();
            _heroAnimator.PlayAttack(this);
            _duration = 1f;
            _attackTransition = 0.5f;
            _canChain = false;
            Debug.Log("Entered FirstState");
        }

        public override void Tick(float deltaTime)
        {
            _duration -= deltaTime;

            if (_input.isAttackButtonUp())
            {
                _canChain = true;
            }

            if (_canChain)
            {
                _attackTransition -= deltaTime;
                if (_attackTransition <= 0)
                {
                    _heroStateMachine.Enter<SecondAttackState>();
                }
            }
            if (StateEnds())
            {
                _heroStateMachine.Enter<HeroIdleState>();
            }
        }

        public override void Exit()
        {

            //_heroLook.OnAttackExitRotation();
        }

        private bool StateEnds()
        {
            return _duration <= 0;
        }
    }
}
