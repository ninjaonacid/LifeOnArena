using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class FirstAttackState : HeroBaseAttackState
    {
        public FirstAttackState(
            HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator, HeroAttack heroAttack) : base(heroStateMachine, input, heroAnimator, heroAttack)
        {
        }

        public override void Enter()
        {
            HeroAnimator.PlayAttack(this);
            HeroAttack.DoAttack(this);
            Duration = 0.7f;
            Debug.Log("Entered FirstState");
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
