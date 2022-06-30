using CodeBase.Infrastructure.States;
using CodeBase.Services;

namespace CodeBase.Infrastructure.Services
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;

        void Enter<TState, TPayload>(TPayload payload) where TState :
            class, IPayloadedState<TPayload>;
    }
}