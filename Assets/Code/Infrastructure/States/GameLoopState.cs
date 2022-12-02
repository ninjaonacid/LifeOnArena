﻿using System;
using System.Threading;
using Code.Services;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameEventHandler _gameEventHandler;
        
        public GameLoopState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameEventHandler gameEventHandler)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameEventHandler = gameEventHandler;
        }

        public void Enter()
        {
            _gameEventHandler.PlayerDead += ReturnToMainMenu;
        }

        private async void ReturnToMainMenu()
        {
            var cts = new CancellationTokenSource();
            await UniTask.Delay(TimeSpan.FromSeconds(5), ignoreTimeScale: false, cancellationToken: cts.Token);
            _stateMachine.Enter<MainMenuState>();
        }

        
        public void Exit()
        {
        }


        
    }
}