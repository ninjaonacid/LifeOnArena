using System;
using System.Collections.Generic;
using System.Linq;
using Code.CustomEvents;

namespace Code.Infrastructure.EventProvider
{
    public class EventService : IEventService
    {
        private readonly Dictionary<Type, List<ISubscription<IEvent>>> _subscriptions;
        private Dictionary<Type, Action<IEvent>> _cachedTypes;

        public EventService()
        {
            _subscriptions = new Dictionary<Type, List<ISubscription<IEvent>>>();
        }
        public void FireEvent<TEvent>(TEvent eventItem) where TEvent : IEvent
        {
            var type = eventItem.GetType();

            var allSubscribers = new List<ISubscription<IEvent>>();

            if (_subscriptions.ContainsKey(typeof(TEvent)))
            {
                allSubscribers = _subscriptions[typeof(TEvent)];
            }
            foreach (var subscriber in allSubscribers)
            {
                subscriber.Publish(eventItem);
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (!_subscriptions.ContainsKey(typeof(TEvent)))
            {
                _subscriptions.Add(typeof(TEvent), new List<ISubscription<IEvent>>());
            }

            //cachedTypes.Add(typeof(TEvent), action);

            _subscriptions[typeof(TEvent)].Add((new Subscription<TEvent>(action)));

        }

        public void Unsubscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent
        {
            var type = action.GetType();

            if (_subscriptions.ContainsKey(type))
            {
                var allEventSubscriptions = _subscriptions[type];
                var removeSubscription = allEventSubscriptions.FirstOrDefault(x => x.SubscriptionToken == action);
                if (removeSubscription != null)
                {
                    _subscriptions[type].Remove(removeSubscription);
                }
            }
        }
    }

    public interface IEventService
    {
        void FireEvent<TEvent>(TEvent eventPublisher) where TEvent : IEvent;

        void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent;

        void Unsubscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent;

    }


    public class TestEventClass : IDoorOpenEvent
    {


        public void OnEnable()
        {
           
        }

        public void OnThing(IDoorOpenEvent events)
        {

        }
    }

    public interface IDoorOpenEvent : IEvent
    {
    }
}
