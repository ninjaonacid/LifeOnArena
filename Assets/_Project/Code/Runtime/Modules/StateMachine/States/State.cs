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

        public ITimer timer;

        public State(bool needExitTime, bool isGhostState, Action<State<TStateId, TEvent>> onEnter,
            Action<State<TStateId, TEvent>> onLogic, Action<State<TStateId, TEvent>> onExit,
            Func<State<TStateId, TEvent>, bool> canExit, ITimer timer) : base(needExitTime, isGhostState)
        {
            _onEnter = onEnter;
            _onLogic = onLogic;
            _onExit = onExit;
            _canExit = canExit;
            this.timer = timer;
        }

        public override void OnEnter()
        {
            timer.Reset();

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
        public State(bool needExitTime,
            bool isGhostState,
            Action<State<TStateId, string>> onEnter,
            Action<State<TStateId, string>> onLogic,
            Action<State<TStateId, string>> onExit,
            Func<State<TStateId, string>, bool> canExit, ITimer timer) :
            base(needExitTime, isGhostState, onEnter, onLogic, onExit, canExit, timer)
        {
        }
    }

    public class State : State<string, string>
    {
        public State(bool needExitTime,
            bool isGhostState,
            Action<State<string, string>> onEnter,
            Action<State<string, string>> onLogic,
            Action<State<string, string>> onExit,
            Func<State<string, string>, bool> canExit, ITimer timer) :
            base(needExitTime, isGhostState, onEnter, onLogic, onExit, canExit, timer)
        {
        }
    }
}