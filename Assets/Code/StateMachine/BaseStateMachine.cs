
using UnityEngine;

namespace Code.StateMachine
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        private State _currentState;
        public State CurrentState => _currentState;
        public virtual void Update()
        {
            _currentState?.Tick(Time.deltaTime);
        }

        public void ChangeState(State newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void InitState(State newState)
        {
            _currentState ??= newState;
        }

    }
}
