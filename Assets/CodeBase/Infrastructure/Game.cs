using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.Services;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner),
                curtain, new AllServices());
        }
    }
}