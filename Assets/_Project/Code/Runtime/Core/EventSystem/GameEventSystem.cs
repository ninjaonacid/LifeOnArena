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

            if (!_subscriptions.TryGetValue(type, out var subscriptions))
            {
                return;
            }

            foreach (var sub in subscriptions)
            {
                try
                {
                    sub.Publish(eventItem);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }

            subscriptions.Cleanup();
        }

        public void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent
        {
            var type = typeof(TEvent);

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (!_subscriptions.TryGetValue(type, out var subscriptions))
            {
                subscriptions = new SubscriptionsList<ISubscription<IEvent>>();
                _subscriptions.Add(type, subscriptions);
            }

            _cachedTypes.Add(action, type);
            subscriptions.Add(new Subscription<TEvent>(action));
        }

        public void Unsubscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent
        {
            if (!_cachedTypes.TryGetValue(action, out var type))
            {
                return;
            }

            if (!_subscriptions.TryGetValue(type, out var subscriptions))
            {
                return;
            }

            if (!_subscriptions.TryGetValue(type, out var subscription)) return;
            ISubscription<IEvent> subToRemove =
                subscriptions.FirstOrDefault(x =>
                    x.SubscriptionToken.Equals(action));

            if (subToRemove != null)
            {
                subscription.Remove(subToRemove);
            }

            subscriptions.Cleanup();
        }
    }
}
