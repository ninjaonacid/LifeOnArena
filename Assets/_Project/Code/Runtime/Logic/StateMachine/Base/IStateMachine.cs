namespace Code.Runtime.Logic.StateMachine.Base
{
    public interface IStateMachine<TState>
    {
        void StateCanExit();

        void RequestStateChange(TState state, bool forceTransition = false);

        void OnLogic();

        StateBase<TState> ActiveState { get; }
        TState ActiveStateName { get; }
    }
}