using Code.Infrastructure.States;
using VContainer.Unity;

namespace Code.Infrastructure
{
    public class GameBootstrapper : IInitializable
    {
        private Game _game;
        private readonly IGameStateMachine _stateMachine;

        public GameBootstrapper(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            _game = new Game(_stateMachine);
            _game.StartGame();
        }
    }
}