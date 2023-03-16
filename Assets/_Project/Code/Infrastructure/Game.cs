using Code.Infrastructure.Services;
using Code.Infrastructure.States;
using Code.Logic;
using Code.Services;

namespace Code.Infrastructure
{
    public class Game
    {
        private IGameStateMachine _stateMachine;

        
        public Game(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _stateMachine.Enter<BootstrapState>();
        }
    }
}