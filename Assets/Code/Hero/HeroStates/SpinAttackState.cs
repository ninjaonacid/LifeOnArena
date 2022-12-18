using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.HeroStates
{
    public class SpinAttackState : HeroBaseAttackState
    {
        private readonly HeroRotation _heroRotation;
        public SpinAttackState(HeroStateMachine heroStateMachine, 
            IInputService input, 
            HeroAnimator heroAnimator, 
            HeroRotation heroRotation,
            HeroAttack heroAttack) : base(heroStateMachine, input, heroAnimator, heroAttack)
        {
            _heroRotation = heroRotation;
        }

        public override void Enter()
        {
            _heroRotation.enabled = false;
            HeroAnimator.PlayAttack(this);
            HeroAttack.SkillAttack();
            Debug.Log("Enter spinAttack");
            Duration = 1f;
        }

        public override void Tick(float deltaTime)
        {
            Duration -= deltaTime;
        }

        public override void Exit()
        {
            _heroRotation.enabled = true;
        }
    }
}
