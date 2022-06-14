namespace CodeBase.Infrastructure.States
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
        void Exit();
    }
}