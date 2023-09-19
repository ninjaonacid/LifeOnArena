namespace Code.Infrastructure.States
{
    public interface IGameState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedGameState<TPayLoad> : IExitableState
    {
        void Enter(TPayLoad payload);
    }

    public interface IExitableState
    {
        IGameStateMachine GameStateMachine { get; set; }
        void Exit();
    }
}