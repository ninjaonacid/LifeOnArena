using System;
using System.Collections.Generic;
using Code.StateMachine.Base;
using Code.StateMachine.Transitions;
using UnityEngine;

namespace Code.StateMachine
{
    public class FiniteStateMachine<TState> : IStateMachine<TState>
    {
        private StateBase<TState> _activeState = null;

        private TransitionBase<TState> _currentTransition;

        private static readonly  List<TransitionBase<TState>> noTransitions = 
            new List<TransitionBase<TState>>();

        private List<TransitionBase<TState>> _possibleTransitions = noTransitions;
        private List<TransitionBase<TState>> _transitionsFromAny = new List<TransitionBase<TState>>();

        private Dictionary<TState, StateStorage> _states = new Dictionary<TState, StateStorage>();

        private (TState state, bool hasState) startState = (default, false);
        private (TState state, bool isPending) pendingState = (default, false);

        public StateBase<TState> ActiveState  => _activeState;

        public TState ActiveStateName { get; }

        private class StateStorage
        {
            public StateBase<TState> state;
            public List<TransitionBase<TState>> _transitions;

            public void AddTransition(TransitionBase<TState> transition)
            {
                _transitions = _transitions ?? new List<TransitionBase<TState>>();
                _transitions.Add(transition);
            }
        }

        private void ChangeState(TState name)
        {
            _activeState?.OnExit();

            if (!_states.TryGetValue(name, out var stateStorage))
            {
                throw new ArgumentException("NoState");
            }

            _possibleTransitions = stateStorage._transitions ?? noTransitions;

            _activeState = stateStorage.state;
            _activeState.OnEnter();

            foreach (var transition in _possibleTransitions)
            {
                transition.OnEnter();
            }
        }

        public void StateCanExit()
        {
            if (pendingState.isPending)
            {
                TState state = pendingState.state;

                pendingState = (default, false);

                ChangeState(state);
            }
        }

        public void OnLogic()
        {
            bool hasTransition = TryAnyTransitions();

            if (!hasTransition)
            {
                TryDirectTransitions();
            }


            _activeState.OnLogic();
        }

        public void InitStateMachine()
        {
            if (!startState.hasState)
            {
                Debug.Log("State Machine Dont Have Start State");
                return;
            }

            ChangeState(startState.state);

        }
        public void AddTransition(TransitionBase<TState> transition)
        {
            InitTransition(transition);

            StateStorage stateStorage = GetOrCreateStateStorage(transition.FromState);

            stateStorage.AddTransition(transition);
        }

        public void AddTransitionFromAny(TransitionBase<TState> transition)
        {
            InitTransition(transition);

            _transitionsFromAny.Add(transition);
        }

        public void AddTwoWayTransition(TransitionBase<TState> transition)
        {
            InitTransition(transition);
            AddTransition(transition);

            ReverseTransition<TState> reverse = new ReverseTransition<TState>(transition, false);
            InitTransition(reverse);
            AddTransition(reverse);
        }
        public void AddState(TState name, StateBase<TState> state)
        {
            state.fsm = this;
            state.name = name;
            state.Init();

            StateStorage stateStorage = GetOrCreateStateStorage(name);
            stateStorage.state = state;

            if (_states.Count == 1 && !startState.hasState)
            {
                SetInitState(name);
            }
        }

        private void InitTransition(TransitionBase<TState> transition)
        {
            transition.Init();
        }
        private void SetInitState(TState name)
        {
            startState = (name, true);
        }

        public void RequestStateChange(TState state, bool forceTransition = false)
        {
            if (!_activeState.NeedExitTime || forceTransition)
            {
                ChangeState(state);
            }
            else
            {
                pendingState = (state, true);
                _activeState.OnExitRequest();
            }
        }

        private bool TryDirectTransitions()
        {
            for (int i = 0; i < _possibleTransitions.Count; i++)
            {
                var transition = _possibleTransitions[i];

                if (TryTransition(transition)) return true;
            }

            return false;
        }
        private bool TryAnyTransitions()
        {
            for (int i = 0; i < _transitionsFromAny.Count; i++)
            {
                var transition = _transitionsFromAny[i];

                if (EqualityComparer<TState>.Default.Equals(transition.ToState, _activeState.name))
                    continue;

                if (TryTransition(transition))
                {
                    return true;
                }
            }

            return false;
        }
        private bool TryTransition(TransitionBase<TState> transition)
        {
            if (!transition.ShouldTransition()) 
                return false;

            RequestStateChange(transition.ToState, transition.IsForceTransition);
            return true;
        }

        private StateStorage GetOrCreateStateStorage(TState name)
        {
            if (!_states.TryGetValue(name, out var storage))
            {
                storage = new StateStorage();
                _states.Add(name, storage);
            }
            return storage;
        }
    }

    public class FiniteStateMachine : FiniteStateMachine<string>
    {

    }
}
