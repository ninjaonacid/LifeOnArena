using CodeBase.Infrastructure.Factory;
using CodeBase.CameraLogic;
using CodeBase.Logic;
using UnityEngine;
using System;
using CodeBase.Infrastructure.Services.PersistentProgress;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        
        
        private const string InitialPointTag = "InitialPoint";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader,
            LoadingCurtain curtain, IGameFactory gameFactory, 
            IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader  in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            } 
        }

        private void InitGameWorld()
        {
            var initialPoint = GameObject.FindWithTag(InitialPointTag);

            GameObject hero = _gameFactory.CreateHero(initialPoint);

            _gameFactory.CreateHud();

            CameraFollow(hero);
        }


        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }
    }
}
