using Code.Infrastructure.States;
using Code.Services;

namespace Code.Infrastructure.Services
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;

        void Enter<TState, TPayload>(TPayload payload) where TState :
            class, IPayloadedState<TPayload>;
    }
}