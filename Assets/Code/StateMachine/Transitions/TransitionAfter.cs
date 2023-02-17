using System;
using System.Threading;
using Code.Logic;
using Code.StateMachine.Base;
using Timer = Code.Logic.Timer;

namespace Code.StateMachine.Transitions
{
    public class TransitionAfter<TStateId> : TransitionBase<TStateId>
    {

        public float delay;
        public Func<TransitionAfter<TStateId>, bool> condition;
        public ITimer timer;
        public TransitionAfter(
            TStateId from,
            TStateId to,
            float delay,
            Func<TransitionAfter<TStateId>, bool> condition = null,
            bool forceInstantly = false) : base(from, to, forceInstantly)
        {
            this.delay = delay;
            this.condition = condition;
            this.timer = new Timer();
        }

        public override void OnEnter()
        {
            timer.Reset();
        }

        public override bool ShouldTransition()
        {
            if (timer.Elapsed < delay)
                return false;

            if (condition == null)
                return true;

            return condition(this);
        }
    }

    public class TransitionAfter : TransitionAfter<string>
    {
        public TransitionAfter(
            string @from,
            string to,
            float delay,
            Func<TransitionAfter<string>, bool> condition = null,
            bool forceInstantly = false) : base(@from, to, delay, condition, forceInstantly)
        {
        }
    }
}
