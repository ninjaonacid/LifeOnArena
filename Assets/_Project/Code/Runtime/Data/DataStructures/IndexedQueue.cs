using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Code.Runtime.Data.DataStructures
{
    public class IndexedQueue<T> : List<T>
    {
        public IndexedQueue([NotNull] IEnumerable<T> collection) : base(collection)
        {
        }

        public IndexedQueue()
        {
        }

        public new void Add(T item) { throw new NotSupportedException(); }
        public new void AddRange(IEnumerable<T> collection) { throw new NotSupportedException(); }
        public new void Insert(int index, T item) { throw new NotSupportedException(); }
        public new void InsertRange(int index, IEnumerable<T> collection) { throw new NotSupportedException(); }
        public new void Reverse() { throw new NotSupportedException(); }
        public new void Reverse(int index, int count) { throw new NotSupportedException(); }
        public new void Sort() { throw new NotSupportedException(); }
        public new void Sort(Comparison<T> comparison) { throw new NotSupportedException(); }
        public new void Sort(IComparer<T> comparer) { throw new NotSupportedException(); }

        public new void Sort(int index, int count, IComparer<T> comparer)
        {
            throw new NotSupportedException();
        }

        public void Enqueue(T item)
        {
            base.Add(item);
        }

        public T Dequeue()
        {
            var item = base[0];
            base.RemoveAt(0);
            return item;
        }

        public T Peek()
        {
            return base[0];
        }
    }
}
