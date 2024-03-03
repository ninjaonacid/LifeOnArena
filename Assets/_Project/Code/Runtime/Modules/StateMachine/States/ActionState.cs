using System;
using Code.Runtime.Modules.StateMachine.Base;
using UnityEngine;

namespace Code.Runtime.Modules.StateMachine.States
{
    public class ActionState<TState, TEvent> : StateBase<TState>
    {
        private ActionStorage<TEvent> _actionStorage;
        
        public ActionState(bool needExitTime, bool isGhostState) : base(needExitTime, isGhostState)
        {
        }

        public ActionState<TState, TEvent> AddAction(TEvent trigger, Action action)
        {
            _actionStorage = _actionStorage ?? new ActionStorage<TEvent>();
            _actionStorage.AddAction(trigger, action);
            return this;
        }

        public ActionState<TState, TEvent> AddAction<TData>(TEvent trigger, Action<TData> action)
        {
            _actionStorage = _actionStorage ?? new ActionStorage<TEvent>();
            _actionStorage.AddAction(trigger, action);
            return this;
        }

        public void OnAction(TEvent trigger) => _actionStorage?.RunAction(trigger);

        public void OnAction<TData>(TEvent trigger, TData data) => _actionStorage?.RunAction<TData>(trigger, data);
    }
}
