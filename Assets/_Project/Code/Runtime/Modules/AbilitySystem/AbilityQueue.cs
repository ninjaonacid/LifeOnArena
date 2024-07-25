using System.Collections.Generic;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AbilityQueue
    {
        private readonly Queue<ActiveAbility> _queue = new Queue<ActiveAbility>();
        private readonly int _queueLimit;
        private readonly float _queueTimeWindow;

        public AbilityQueue(int queueLimit, float queueTimeWindow)
        {
            _queueLimit = queueLimit;
            _queueTimeWindow = queueTimeWindow;
        }

        public bool TryEnqueue(ActiveAbility ability)
        {
            if (_queue.Count >= _queueLimit)
                return false;

            _queue.Enqueue(ability);
            return true;
        }

        public ActiveAbility Dequeue()
        {
            return _queue.Count > 0 ? _queue.Dequeue() : null;
        }

        public ActiveAbility Peek()
        {
            return _queue.Count > 0 ? _queue.Peek() : null;
        }

        public void Clear()
        {
            _queue.Clear();
        }

        public bool HasNext()
        {
            return _queue.Count > 0;
        }

    }
}