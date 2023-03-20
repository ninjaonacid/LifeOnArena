using System;
using System.Collections.Generic;
using System.Linq;
using Code.CustomEvents;

namespace Code.Infrastructure.EventProvider
{
    public class EventService : IEventService
    {
        private readonly Dictionary<Type, SubscriptionsList<ISubscription<IEvent>>> _subscriptions;
        private readonly Dictionary<object, Type> _cachedTypes;
        public EventService()
        {
            _subscriptions = new Dictionary<Type, SubscriptionsList<ISubscription<IEvent>>>();
            _cachedTypes = new Dictionary<object, Type>();

        }
        public void FireEvent<TEvent>(TEvent eventItem) where TEvent : IEvent
        {
            var type = eventItem.GetType();

            if (!_subscriptions.ContainsKey(type))
            {
                throw new ArgumentNullException("Cant invoke event, doesnt present in the subscriptions");

            }
            var allSubscriptions = new SubscriptionsList<ISubscription<IEvent>>();

            
            if (_subscriptions.ContainsKey(type))
            {
                allSubscriptions = _subscriptions[type];
                allSubscriptions.IsExecuting = true;
            }

            foreach (var subscription in allSubscriptions)
            {
                try
                {
                    subscription.Publish(eventItem);
                }
                catch(Exception exception)
                {
                    throw new Exception("Invoke event exception");
                }
            }

            allSubscriptions.IsExecuting = false;
            allSubscriptions.Cleanup();
        }

        public void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent
        {

            var type = typeof(TEvent);

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (!_subscriptions.ContainsKey(typeof(TEvent)))
            {
                _subscriptions.Add(type, new SubscriptionsList<ISubscription<IEvent>>());
            }

            
            _cachedTypes.Add(action, type);
            _subscriptions[type].Add((new Subscription<TEvent>(action)));

        }

        public void Unsubscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent
        {
            var type = _cachedTypes[action];


            if (_subscriptions.ContainsKey(type))
            {
                var allEventSubs = _subscriptions[type];
                var subToRemove = allEventSubs.FirstOrDefault(x => x.SubscriptionToken.Equals(action));
                if (subToRemove != null)
                {
                    _subscriptions[type].Remove(subToRemove);
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

}
