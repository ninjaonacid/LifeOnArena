using Code.Services.Input;

namespace Code.Hero.HeroStates
{
    public class MovementState : HeroBaseState
    {
        private readonly HeroMovement _heroMovement;
        public MovementState(HeroStateMachine heroStateMachine,
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

            if (_heroMovement.GetVelocity() <= 0)
            {
                HeroStateMachine.Enter<HeroIdleState>();
            } 
            else if (Input.isAttackButtonUp())
            {
                HeroStateMachine.Enter<FirstAttackState>();
            }

        }

        public override void Exit()
        {
           _heroMovement.StopMove();
        }
    }
}
