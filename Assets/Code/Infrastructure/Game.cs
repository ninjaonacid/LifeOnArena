using Code.Infrastructure.States;
using Code.Logic;
using Code.Services;

namespace Code.Infrastructure
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