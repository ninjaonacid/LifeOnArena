using Code.Runtime.Logic.StateMachine.Base;

namespace Code.Runtime.Logic.StateMachine.Transitions
{
    public class ReverseTransition<TState> : TransitionBase<TState>
        {
            public TransitionBase<TState> wrappedTransition;
            private bool shouldInitWrappedTransition;

            public ReverseTransition(
                TransitionBase<TState> wrappedTransition,
                bool shouldInitWrappedTransition = true)
                : base(
                    from: wrappedTransition.ToState,
                    to: wrappedTransition.FromState,
                    isForceTransition: wrappedTransition.IsForceTransition)
            {
                this.wrappedTransition = wrappedTransition;
                this.shouldInitWrappedTransition = shouldInitWrappedTransition;
            }

            public override void Init()
            {
                
            }

            public override void OnEnter()
            {
                wrappedTransition.OnEnter();
            }

            public override bool ShouldTransition()
            {
                return !wrappedTransition.ShouldTransition();
            }
        }

        public class ReverseTransition : ReverseTransition<string>
        {
            public ReverseTransition(
                TransitionBase<string> wrappedTransition,
                bool shouldInitWrappedTransition = true)
                : base(wrappedTransition, shouldInitWrappedTransition)
            {
            }
        }
	
}
