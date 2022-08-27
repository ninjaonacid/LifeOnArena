using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public class SecondAttackState : HeroBaseState
    {
        private float _duration;
        private bool _canChain;
        private float _attackTransition;
        private HeroLookRotation _heroLook;
        public SecondAttackState(
            HeroStateMachine heroStateMachine,
            IInputService input,
            HeroAnimator heroAnimator, HeroLookRotation heroLook 
            ) : base(heroStateMachine, input, heroAnimator)
        {
            _heroLook = heroLook;
        }

        public override void Enter()
        {
            //_heroLook.OnAttackEnterRotation();
            _heroAnimator.PlayAttack(this);
            _attackTransition = 0.5f;
            _canChain = false;
            _duration = 1f;
            Debug.Log("Entered SecondState");
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
                    _heroStateMachine.Enter<ThirdAttackState>();
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
            return _duration < 0;
        }

    }
}
