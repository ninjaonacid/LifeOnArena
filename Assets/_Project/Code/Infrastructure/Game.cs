using Code.Infrastructure.Services;
using Code.Infrastructure.States;

namespace Code.Infrastructure
{
    public class Game
    {
        private readonly IGameStateMachine _stateMachine;

        
        public Game(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void StartGame()
        {
            _stateMachine.Enter<BootstrapState>();
        }
    }
}