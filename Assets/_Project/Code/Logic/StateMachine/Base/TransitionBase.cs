namespace Code.Logic.StateMachine.Base
{
    public abstract class TransitionBase<TState> 
    {
        public TState FromState { get; private set; }
        public TState ToState { get; private set; }

        public bool IsForceTransition;

        public bool IsRepeatableTransition;
        protected TransitionBase(TState from, TState to, bool isForceTransition = false, bool isRepeatableTransition = false)
        {
            FromState = from;
            ToState = to;
            IsForceTransition = isForceTransition;
            IsRepeatableTransition = isRepeatableTransition;
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
