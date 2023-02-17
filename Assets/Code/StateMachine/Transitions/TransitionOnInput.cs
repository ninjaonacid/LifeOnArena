using Code.StateMachine.Base;
using UnityEngine;

namespace Code.StateMachine.Transitions
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
