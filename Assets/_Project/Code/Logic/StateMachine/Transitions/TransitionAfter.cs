using System;
using Code.Logic.StateMachine.Base;
using Code.Logic.Timer;
using Timer = Code.Logic.Timer.Timer;

namespace Code.Logic.StateMachine.Transitions
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
            this.timer = new Timer.Timer();
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
