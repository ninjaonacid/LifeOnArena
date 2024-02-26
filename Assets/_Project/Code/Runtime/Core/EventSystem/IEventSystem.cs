using System;
using Code.Runtime.CustomEvents;

namespace Code.Runtime.Core.EventSystem
{
    public interface IEventSystem
    {
        void FireEvent<TEvent>(TEvent eventPublisher) where TEvent : IEvent;

        void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent;

        void Unsubscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent;

    }
}