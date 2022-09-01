using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class SecondAttackState : HeroBaseState
    {
        private float _duration;
        private bool _canChain;
        private float _attackTransition;
        public SecondAttackState(
            HeroStateMachine heroStateMachine,
            IInputService input,
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
        }

        public override void Enter()
        {
            HeroAnimator.PlayAttack(this);
            _attackTransition = 0.7f;
            _canChain = false;
            _duration = 1f;
            Debug.Log("Entered SecondState");
        }

        public override void Tick(float deltaTime)
        {
            _duration -= deltaTime;

            if (Input.isAttackButtonUp())
            {
                _canChain = true;
            }

            if (_canChain)
            {
                _attackTransition -= deltaTime;
                if (_attackTransition <= 0)
                {
                    HeroStateMachine.Enter<ThirdAttackState>();
                }
            }
            if (StateEnds())
            {
                HeroStateMachine.Enter<HeroIdleState>();
            }
        }

        public override void Exit()
        {
            
        }

        private bool StateEnds()
        {
            return _duration < 0;
        }

    }
}
