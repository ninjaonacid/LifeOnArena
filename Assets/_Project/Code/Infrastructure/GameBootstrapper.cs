using Code.Infrastructure.Services;
using Code.Logic;
using VContainer.Unity;

namespace Code.Infrastructure
{
    public class GameBootstrapper : IInitializable
    {
        private Game _game;

        private LoadingCurtain _curtain;

        private readonly IGameStateMachine _stateMachine;

        public GameBootstrapper(IGameStateMachine stateMachine, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _curtain = curtain;
        }

        public void Initialize()
        {
            _game = new Game(_stateMachine);
            _game.StartGame();
        }
    }
}