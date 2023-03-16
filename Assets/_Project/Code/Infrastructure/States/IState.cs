using Code.Infrastructure.Services;

namespace Code.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayLoad> : IExitableState
    {
        void Enter(TPayLoad payload);
    }

    public interface IExitableState
    {
        IGameStateMachine GameStateMachine { get; set; }
        void Exit();
    }
}