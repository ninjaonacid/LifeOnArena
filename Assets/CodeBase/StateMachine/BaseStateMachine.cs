
using UnityEngine;


namespace CodeBase.StateMachine
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        private State _currentState;

        private void Update()
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
