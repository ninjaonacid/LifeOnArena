using System;
using Code.Runtime.CustomEvents;

namespace Code.Runtime.Core.EventSystem
{
    public class Subscription<TEvent> : ISubscription<IEvent> 
    {
        private readonly Action<TEvent> _action;
        public object SubscriptionToken
        {
            get { return _action; }
        }
        public Subscription(Action<TEvent> action)
        {
            _action = action ?? throw new ArgumentNullException("action");
        }

 
        public void Publish(IEvent eventItem)
        {
            if (!(eventItem is TEvent))
            {
                throw new ArgumentException("Incorrect event type");
            }

            _action.Invoke((TEvent)eventItem);
        }
    }

    public interface ISubscription<TEvent>
    {
        void Publish(TEvent eventItem);
        object SubscriptionToken { get; }
    }
}
