namespace Code.Entity.Hero.HeroStates
{
    public class HeroIdleState : HeroBaseState
    {
        public HeroIdleState(HeroAnimator animator, bool needExitTime, bool isGhostState) : base(animator, needExitTime : false, isGhostState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            HeroAnimator.ToIdleState();
        }

        public override void OnLogic()
        {
            base.OnLogic();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
