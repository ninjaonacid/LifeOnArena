using System;
using System.Collections.Generic;
using Code.Runtime.Modules.StateMachine.Base;
using Code.Runtime.Modules.StateMachine.Transitions;
using UnityEngine;

namespace Code.Runtime.Modules.StateMachine
{
    public class FiniteStateMachine<TState, TEvent> : IStateMachine<TState>, ITriggerable<TEvent>
    {
        private StateBase<TState> _activeState = null;

        private TransitionBase<TState> _currentTransition;

        private static readonly  List<TransitionBase<TState>> noTransitions = 
            new List<TransitionBase<TState>>();

        private static readonly Dictionary<TEvent, List<TransitionBase<TState>>> noTriggerTransitions =
            new Dictionary<TEvent, List<TransitionBase<TState>>>();


        private Dictionary<TEvent, List<TransitionBase<TState>>> _triggerTransitions = noTriggerTransitions;
        private List<TransitionBase<TState>> _possibleTransitions = noTransitions;

        private Dictionary<TEvent, List<TransitionBase<TState>>> _triggerTransitionsFromAny =
            new Dictionary<TEvent, List<TransitionBase<TState>>>();

        private List<TransitionBase<TState>> _transitionsFromAny = 
            new List<TransitionBase<TState>>();
        
       

        private Dictionary<TState, StateStorage> _states = new Dictionary<TState, StateStorage>();

        private (TState state, bool hasState) startState = (default, false);
        private (TState state, bool isPending) pendingState = (default, false);

        public StateBase<TState> ActiveState  => _activeState;

        public TState ActiveStateName => _activeState.name;

        private class StateStorage
        {
            public StateBase<TState> State;
            public List<TransitionBase<TState>> Transitions;
            public Dictionary<TEvent, List<TransitionBase<TState>>> TriggerTransitions;

            public void AddTransition(TransitionBase<TState> transition)
            {
                Transitions = Transitions ?? new List<TransitionBase<TState>>();
                Transitions.Add(transition);
            }

            public void AddTriggerTransition(TEvent trigger, TransitionBase<TState> transition)
            {
                TriggerTransitions = TriggerTransitions
                                     ?? new Dictionary<TEvent, List<TransitionBase<TState>>>();
            }
        }

        private void ChangeState(TState name)
        {
            _activeState?.OnExit();

            if (!_states.TryGetValue(name, out var stateStorage))
            {
                throw new ArgumentException("NoState");
            }

            _possibleTransitions = stateStorage.Transitions ?? noTransitions;

            _activeState = stateStorage.State;
            _activeState.OnEnter();

            foreach (var transition in _possibleTransitions)
            {
                transition.OnEnter();
            }

            foreach (List<TransitionBase<TState>> transitions in _triggerTransitions.Values)
            {
                foreach (var transition in transitions)
                {
                    transition.OnEnter();
                }
            }

            if (ActiveState.IsGhostState)
            {
                TryDirectTransitions();
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
            stateStorage.State = state;

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

        public void AddTriggerTransition(TEvent trigger, TransitionBase<TState> transition)
        {
            InitTransition(transition);

            StateStorage storage = GetOrCreateStateStorage(transition.FromState);
            storage.AddTriggerTransition(trigger, transition);
        }

        public void AddTriggerTransitionFromAny(TEvent trigger, TransitionBase<TState> transition)
        {
            InitTransition(transition);

            List<TransitionBase<TState>> transitionsOfTrigger;

            if (!_triggerTransitionsFromAny.TryGetValue(trigger, out transitionsOfTrigger))
            {
                transitionsOfTrigger = new List<TransitionBase<TState>>();
                _triggerTransitionsFromAny.Add(trigger, transitionsOfTrigger);
            }

            transitionsOfTrigger.Add(transition);
        }

        private bool TryTrigger(TEvent trigger)
        {

            List<TransitionBase<TState>> triggerTransitions;

            if (_triggerTransitionsFromAny.TryGetValue(trigger, out triggerTransitions))
            {
                for (int i = 0; i < triggerTransitions.Count; i++)
                {
                    TransitionBase<TState> transition = triggerTransitions[i];
                    

                    if (EqualityComparer<TState>.Default.Equals(transition.ToState, _activeState.name))
                        continue;

                    if (TryTransition(transition))
                        return true;
                }
            }

            if (_triggerTransitions.TryGetValue(trigger, out triggerTransitions))
            {
                for (int i = 0; i < triggerTransitions.Count; i++)
                {
                    TransitionBase<TState> transition = triggerTransitions[i];

                    if (TryTransition(transition))
                        return true;
                }
            }

            return false;
        }

        public void Trigger(TEvent trigger)
        {
            if (TryTrigger(trigger)) return;

            (_activeState as ITriggerable<TEvent>)?.Trigger(trigger);
        }
    }
    
    public class FiniteStateMachine<TStateId> : FiniteStateMachine<TStateId, string>
    {
    }

    public class FiniteStateMachine : FiniteStateMachine<string, string>
    {

    }

}
