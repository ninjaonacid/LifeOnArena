using Code.Services.Input;

namespace Code.Hero.HeroStates
{
    public class HeroMovementState : HeroBaseState
    {
        private readonly HeroMovement _heroMovement;
        public HeroMovementState(HeroStateMachine heroStateMachine,
            IInputService input,
            HeroAnimator heroAnimator, 
            HeroMovement heroMovement) : base(heroStateMachine, input, heroAnimator)
        {
            _heroMovement = heroMovement;
        }

        public override void Enter()
        {
            HeroAnimator.PlayRun();
        }

        public override void Tick(float deltaTime)
        {
            _heroMovement.Movement();

            if (_heroMovement.GetVelocity() <= 0 || Input.isAttackButtonUp() || Input.isSkillButton1())
            {
                HeroStateMachine.DoTransition(this);
            }

        }

        public override void Exit()
        {
           _heroMovement.StopMove();
        }
    }
}
