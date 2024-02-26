using Code.Runtime.Modules.StateMachine.Base;

namespace Code.Runtime.Modules.StateMachine.Transitions
{
    public class CycleTransition<TState> : TransitionBase<TState>
    {
        public CycleTransition(TState from, TState to, bool isForceTransition = false) : base(from, to, isForceTransition)
        {
        }

        public override void Init()
        {
            base.Init();
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override bool ShouldTransition()
        {
            return base.ShouldTransition();
        }

       
    }

    public class CycleTransition : CycleTransition<string>
    {
        public CycleTransition(string from, string to, bool isForceTransition = false) : base(from, to, isForceTransition)
        {
        }
    }
}
