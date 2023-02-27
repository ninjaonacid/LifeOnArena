using System;
using Code.StateMachine.Base;
using UnityEngine;

namespace Code.StateMachine
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
