using Code.Services.Input;

namespace Code.Hero.HeroStates
{
    public class SpinAttackState : HeroBaseAttackState
    {
        public SpinAttackState(HeroStateMachine heroStateMachine, IInputService input, HeroAnimator heroAnimator) : base(heroStateMachine, input, heroAnimator)
        {
        }

        public override void Enter()
        {
            HeroAnimator.PlaySpinAttackSkill();
            Duration = 1f;
 
        }

        public override void Tick(float deltaTime)
        {
            Duration -= deltaTime;
            if (Duration <= 0f)
            {
                HeroStateMachine.Enter<HeroIdleState>();
            }
        }

        public override void Exit()
        {
        }
    }
}
