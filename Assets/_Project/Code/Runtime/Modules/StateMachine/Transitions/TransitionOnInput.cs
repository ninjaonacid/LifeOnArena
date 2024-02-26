using Code.Runtime.Modules.StateMachine.Base;

namespace Code.Runtime.Modules.StateMachine.Transitions
{
    public class TransitionOnInput<TState> : TransitionBase<TState>
    {
        public TransitionOnInput(
            TState from, 
            TState to, 

            bool isForceTransition = false) : base(from, to, isForceTransition)
        {
        }


    }
}
