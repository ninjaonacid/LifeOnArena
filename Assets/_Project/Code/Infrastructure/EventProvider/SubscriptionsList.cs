using System.Collections;
using System.Collections.Generic;
using Code.CustomEvents;

namespace Code.Infrastructure.EventProvider
{
    public class SubscriptionsList<TSubscription> : IEnumerable<TSubscription> where TSubscription : class, ISubscription<IEvent>
    {
        private readonly List<TSubscription> _subscriptions = new List<TSubscription>();
        private readonly List<TSubscription> _subsToRemove = new List<TSubscription>();
        public bool IsExecuting;
        private bool _isForCleanup;
        public void Add(TSubscription subscription)
        {
            _subscriptions.Add(subscription);
        }

        public void Remove(TSubscription subscription)
        {
            if (IsExecuting)
            {
                var elementIndex = _subscriptions.IndexOf(subscription);

                if (elementIndex != -1)
                {
                    _isForCleanup = true;
                    _subsToRemove.Add(subscription);
                }
            }

            else
            {
                _subscriptions.Remove(subscription);
            }
        }

        public void Cleanup()
        {
            if (!_isForCleanup)
            {
                return;
            }

            foreach (var remover in _subsToRemove)
            {
                _subscriptions.Remove(remover);
            }

            _subsToRemove.Clear();

            _isForCleanup = false;
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
