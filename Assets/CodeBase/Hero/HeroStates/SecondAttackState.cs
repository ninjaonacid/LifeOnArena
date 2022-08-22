using CodeBase.Services.Input;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace CodeBase.Hero.HeroStates
{
    public class SecondAttackState : HeroBaseState
    {
        private readonly HeroStateMachine _heroStateMachine;
        private readonly IInputService _inputService;
        private readonly HeroAnimator _heroAnimator;
        private float duration = 0.5f;

        public SecondAttackState(HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
            _heroStateMachine = heroStateMachine;
            _inputService = input;
            _heroAnimator = heroAnimator;
        }

        public override void Enter()
        {
            _heroAnimator.PlayAttack(this);
            Debug.Log("Entered secondState");
        }

        public override void Tick(float deltaTime)
        {
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                _heroStateMachine.ChangeState(new HeroIdleState(_heroStateMachine, _inputService, _heroAnimator));
            }
        }

        public override void Exit()
        {
            
        }
    }
}
