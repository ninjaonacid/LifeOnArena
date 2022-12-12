using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class ThirdAttackState : HeroBaseAttackState
    {
        public ThirdAttackState(HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator, 
            HeroAttack heroAttack) : base(heroStateMachine, input, heroAnimator, heroAttack)
        {
        }

        public override void Enter()
        {
            Duration = 0.7f;
            HeroAnimator.PlayAttack(this);
            HeroAttack.BaseAttack();
            Debug.Log("Entered ThirdState");
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
