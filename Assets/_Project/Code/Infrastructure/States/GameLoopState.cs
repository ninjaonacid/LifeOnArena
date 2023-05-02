using System;
using System.Threading;
using Code.Infrastructure.SceneManagement;
using Code.Services;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States
{
    public class GameLoopState : IState
    {
       
        private readonly SceneLoader _sceneLoader;
        private readonly ILevelEventHandler _levelEventHandler;

        public IGameStateMachine GameStateMachine { get; set; }

        public GameLoopState(SceneLoader sceneLoader, ILevelEventHandler levelEventHandler)
        {
            _sceneLoader = sceneLoader;
            _levelEventHandler = levelEventHandler;
        }

        public void Enter()
        {
            _levelEventHandler.PlayerDead += ReturnToMainMenu;
        }

        private async void ReturnToMainMenu()
        {

            var cts = new CancellationTokenSource();
            await UniTask.Delay(TimeSpan.FromSeconds(5), ignoreTimeScale: false, cancellationToken: cts.Token);
            GameStateMachine.Enter<MainMenuState>();
        }


        public void Exit()
        {
        }

    }
}