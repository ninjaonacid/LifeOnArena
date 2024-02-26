using System.Collections;
using System.Collections.Generic;
using Code.Runtime.CustomEvents;

namespace Code.Runtime.Core.EventSystem
{
    public class SubscriptionsList<TSubscription> : IEnumerable<TSubscription> where TSubscription : class, ISubscription<IEvent>
    {
        private readonly List<TSubscription> _subscriptions;
        private readonly List<TSubscription> _subsToRemove;

        public SubscriptionsList()
        {
            _subscriptions = new List<TSubscription>();
            _subsToRemove = new List<TSubscription>();
        }
        public void Add(TSubscription subscription)
        {
            _subscriptions.Add(subscription);
        }

        public void Remove(TSubscription subscription)
        {
            _subsToRemove.Add(subscription);
        }

        public void Cleanup()
        {
            foreach (var subToRemove in _subsToRemove)
            {
                _subscriptions.Remove(subToRemove);
            }
            _subsToRemove.Clear();
        }

        public IEnumerator<TSubscription> GetEnumerator()
        {
            foreach (var subscription in _subscriptions)
            {
                yield return subscription;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
