using System;

namespace Code.StateMachine.Base
{
    public abstract class TransitionBase<TState> 
    {
        public TState FromState { get; private set; }
        public TState ToState { get; private set; }

        public bool IsForceTransition;

        protected TransitionBase(TState from, TState to, bool isForceTransition = false)
        {
            FromState = from;
            ToState = to;
            IsForceTransition = isForceTransition;
        }

        public virtual void Init()
        {

        }

        public virtual void OnEnter()
        {

        }

        public virtual bool ShouldTransition()
        {
            return true;
        }



    }
}
