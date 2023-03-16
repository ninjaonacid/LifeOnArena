using Code.Infrastructure.Services;
using Code.Services;
using Code.Services.Input;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initialize";
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        public IGameStateMachine GameStateMachine { get; set; }


        public BootstrapState(SceneLoader sceneLoader,
            IStaticDataService staticData)
        {
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _staticData.Load();

        }

        public void Enter()
        {
            _sceneLoader.Load(InitialScene, EnterLoadLevel);
            
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            GameStateMachine.Enter<LoadProgressState>();
        }

        
    }
}