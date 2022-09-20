using Code.Services.Input;

namespace Code.Hero.HeroStates
{
    public class SpinAttackState : HeroBaseAttackState
    {
        private readonly HeroRotation _heroRotation;
        public SpinAttackState(HeroStateMachine heroStateMachine, IInputService input, HeroAnimator heroAnimator, HeroRotation heroRotation) : base(heroStateMachine, input, heroAnimator)
        {
            _heroRotation = heroRotation;
        }

        public override void Enter()
        {
            _heroRotation.enabled = false;
            HeroAnimator.PlaySpinAttackSkill();
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
