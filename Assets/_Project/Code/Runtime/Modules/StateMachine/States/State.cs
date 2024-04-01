using System;
using Code.Runtime.Logic.Timer;

namespace Code.Runtime.Modules.StateMachine.States
{
    public class State<TStateId, TEvent> : ActionState<TStateId, TEvent>
    {
        private readonly Action<State<TStateId, TEvent>> _onEnter;
        private readonly Action<State<TStateId, TEvent>> _onLogic;
        private readonly Action<State<TStateId, TEvent>> _onExit;
        private readonly Func<State<TStateId, TEvent>, bool> _canExit;

        public ITimer Timer;

        public State(bool needExitTime = false, bool isGhostState = false,
            Action<State<TStateId, TEvent>> onEnter = null,
            Action<State<TStateId, TEvent>> onLogic = null,
            Action<State<TStateId, TEvent>> onExit = null,
            Func<State<TStateId, TEvent>, bool> canExit = null) : base(needExitTime, isGhostState)
        {
            _onEnter = onEnter;
            _onLogic = onLogic;
            _onExit = onExit;
            _canExit = canExit;
            this.Timer = new Timer();
        }

        public override void OnEnter()
        {
            Timer.Reset();

            _onEnter?.Invoke(this);
        }

        public override void OnLogic()
        {
            if (NeedExitTime && _canExit != null && _canExit(this))
            {
                fsm.StateCanExit();
            }

            _onLogic?.Invoke(this);
        }

        public override void OnExit()
        {
            _onExit?.Invoke(this);
        }

        public override void OnExitRequest()
        {
            if (_canExit != null && _canExit(this))
            {
                fsm.StateCanExit();
            }
        }
    }

    public class State<TStateId> : State<TStateId, string>
    {
        public State(bool needExitTime = false, bool isGhostState = false,
            Action<State<TStateId, string>> onEnter = null, Action<State<TStateId, string>> onLogic = null,
            Action<State<TStateId, string>> onExit = null, Func<State<TStateId, string>, bool> canExit = null) : base(
            needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
        }
    }

    public class State : State<string, string>
    {
        public State(bool needExitTime = false, bool isGhostState = false, Action<State<string, string>> onEnter = null,
            Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null,
            Func<State<string, string>, bool> canExit = null) : base(needExitTime, isGhostState, onEnter, onLogic,
            onExit, canExit)
        {
        }
    }
}