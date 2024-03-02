using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.CustomEvents;
using UnityEngine;

namespace Code.Runtime.Core.EventSystem
{
    public class GameEventSystem : IEventSystem
    {
        private readonly Dictionary<Type, SubscriptionsList<ISubscription<IEvent>>> _subscriptions;
        private readonly Dictionary<object, Type> _cachedTypes;
        public GameEventSystem()
        {
            _subscriptions = new Dictionary<Type, SubscriptionsList<ISubscription<IEvent>>>();
            _cachedTypes = new Dictionary<object, Type>();

        }
        public void FireEvent<TEvent>(TEvent eventItem) where TEvent : IEvent
        {
            var type = eventItem.GetType();

            if (!_subscriptions.ContainsKey(type))
            {
                throw new Exception("Cant invoke event, doesnt present in the subscriptions");

            }
            var allSubscriptions = new SubscriptionsList<ISubscription<IEvent>>();

            
            if (_subscriptions.TryGetValue(type, out var subscriptions))
            {
                allSubscriptions = subscriptions;
            }

            foreach (var sub in allSubscriptions)
            {
                try
                {
                    sub.Publish(eventItem);
                }
                catch(Exception exception)
                {
                    Debug.LogException(exception);
                }
            }

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

            SubscriptionsList<ISubscription<IEvent>> allSubscriptions = _subscriptions[type];

            if (_subscriptions.ContainsKey(type))
            {
                
                ISubscription<IEvent> subToRemove = 
                    allSubscriptions.FirstOrDefault(x =>
                    x.SubscriptionToken.Equals(action));

                if (subToRemove != null)
                {
                    _subscriptions[type].Remove(subToRemove);
                }
            }
        }
    }
}