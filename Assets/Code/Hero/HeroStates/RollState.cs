using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class RollState : HeroBaseAttackState
    {
        public RollState(HeroStateMachine heroStateMachine, IInputService input, HeroAnimator heroAnimator, HeroAttack heroAttack) : base(heroStateMachine, input, heroAnimator, heroAttack)
        {
        }

        public override void Enter()
        {
            HeroAnimator.PlayRoll();
            Duration = 1f;
        }

        public override void Tick(float deltaTime)
        {
            Duration -= deltaTime;
        }

        public override void Exit()
        {
        }
    }
}
