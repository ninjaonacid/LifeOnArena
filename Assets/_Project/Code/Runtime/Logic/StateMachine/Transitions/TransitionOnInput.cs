using Code.Runtime.Logic.StateMachine.Base;

namespace Code.Runtime.Logic.StateMachine.Transitions
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
