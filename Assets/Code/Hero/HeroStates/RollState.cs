using Code.Services.Input;

namespace Code.Hero.HeroStates
{
    public class RollState : HeroBaseAttackState
    {
        private readonly HeroMovement _heroMovement;
        private readonly HeroRotation _heroRotation;
        public RollState(HeroStateMachine heroStateMachine, IInputService input, HeroAnimator heroAnimator, HeroAttack heroAttack, HeroMovement heroMovement, HeroRotation heroRotation) : base(heroStateMachine, input, heroAnimator, heroAttack)
        {
            _heroRotation = heroRotation;
            _heroMovement = heroMovement;
        }

        public override void Enter()
        {
            HeroAnimator.PlayRoll();
            _heroRotation.enabled = false;
            
            Duration = 1f;
        }

        public override void Tick(float deltaTime)
        {
            Duration -= deltaTime;
            _heroMovement.ForceMove();
        }

        public override void Exit()
        {
            _heroRotation.enabled = true;
        }
    }
}
