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
      

        public IGameStateMachine GameStateMachine { get; set; }

        public GameLoopGameState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
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