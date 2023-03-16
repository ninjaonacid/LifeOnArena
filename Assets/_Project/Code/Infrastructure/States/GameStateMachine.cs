using System;
using System.Collections.Generic;
using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>();
        private IExitableState _activeState;

        public GameStateMachine(IEnumerable<IExitableState> states)
        {
            foreach (var state in states)
            {
                AddState(state);
            }
        }

        private void AddState(IExitableState state)
        {
            state.GameStateMachine = this;
            _states.Add(state.GetType(), state);
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
            Debug.Log(state.ToString());
        }


        public void Enter<TState, TPayload>(TPayload payload) where TState :
            class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            Debug.Log(state.ToString());
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }
    }
}