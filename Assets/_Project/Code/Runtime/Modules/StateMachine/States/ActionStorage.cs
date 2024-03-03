using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Modules.StateMachine.States
{
    public class ActionStorage<TEvent>
    {
        private Dictionary<TEvent, Delegate> _eventActions = new Dictionary<TEvent, Delegate>();

        private TTarget TryGetAndCastAction<TTarget>(TEvent trigger) where TTarget : Delegate
        {
            Delegate action = null;
            
            if(!_eventActions.TryGetValue(trigger, out action))
            {
                return null;
            }

            TTarget target = action as TTarget;

            if (target is null)
            {
                throw new InvalidOperationException();
            }

            return target;
        }

        public void AddAction(TEvent trigger, Action action)
        {
            _eventActions.Add(trigger, action);
        }

        public void AddAction<TData>(TEvent trigger, Action<TData> action)
        {
            _eventActions.Add(trigger, action);
        }

        public void RunAction(TEvent trigger) => TryGetAndCastAction<Action>(trigger).Invoke();

        public void RunAction<TData>(TEvent trigger, TData data) => 
            TryGetAndCastAction<Action<TData>>(trigger).Invoke(data);
    }
}
