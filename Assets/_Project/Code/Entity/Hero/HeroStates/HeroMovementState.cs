namespace Code.Entity.Hero.HeroStates
{
    public class HeroMovementState : HeroBaseState
    {
        public HeroMovementState(HeroAnimator heroAnimator, HeroMovement heroMovement, HeroRotation heroRotation, bool needsExitTime, bool isGhostState = false) : base(heroAnimator, heroMovement, heroRotation, needsExitTime, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _heroAnimator.PlayRun();
        }

        public override void OnLogic()
        {
            _heroMovement.Movement();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
        }
    }
}
