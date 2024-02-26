using System;
using Code.Runtime.Logic.StateMachine.Base;

namespace Code.Runtime.Logic.StateMachine
{
    public class Transition<TState> : TransitionBase<TState>
    {
        public Func<Transition<TState>, bool> condition;


        public Transition(TState from, TState to, Func<Transition<TState>, bool> condition = null, bool isForceTransition = false) : base(from, to, isForceTransition)
        {
            this.condition = condition;
        }

        public override bool ShouldTransition()
        {
            if (condition == null)
                return true;

            return condition(this);
        }
    }

    public class Transition : Transition<string>
    {
        public Transition(string from, string to, Func<Transition<string>, bool> condition = null, bool isForceTransition = false) : base(from, to, condition, isForceTransition)
        {
        }
    }
}
