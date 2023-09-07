using System;
using System.Threading;
using Code.Infrastructure.SceneManagement;
using Code.Services;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States
{
    public class GameLoopGameState : IGameState
    {
       
        private readonly SceneLoader _sceneLoader;
        private readonly ILevelEventHandler _levelEventHandler;

        public IGameStateMachine GameStateMachine { get; set; }

        public GameLoopGameState(SceneLoader sceneLoader, ILevelEventHandler levelEventHandler)
        {
            _sceneLoader = sceneLoader;
            _levelEventHandler = levelEventHandler;
        }

        public void Enter()
        {
            
        }

        private async void ReturnToMainMenu()
        {

            var cts = new CancellationTokenSource();
            await UniTask.Delay(TimeSpan.FromSeconds(5), ignoreTimeScale: false, cancellationToken: cts.Token);
            GameStateMachine.Enter<MainMenuGameState>();
        }


        public void Exit()
        {
        }

    }
}