using Code.StateMachine.Base;
using UnityEngine;

namespace Code.StateMachine.Transitions
{
    public class SwitchableTransition<TState> : TransitionBase<TState>
    {

        public SwitchableTransition(
            TState from, 
            TState to, 
            bool isForceTransition = false) : base(from, to, isForceTransition)
        {

        }
    }
}
