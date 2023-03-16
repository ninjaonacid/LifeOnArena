
using Code.Infrastructure.Services;
using Code.Infrastructure.States;
using Code.Logic;
using UnityEngine;
using VContainer.Unity;

namespace Code.Infrastructure
{
    public class GameBootstrapper : IInitializable
    {
        private Game _game;
        public LoadingCurtain _curtain;

        public IGameStateMachine _stateMachine;

        public GameBootstrapper(IGameStateMachine stateMachine, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _curtain = curtain;
        }


        public void Initialize()
        {
            _game = new Game(_stateMachine);
            
        }
    }
}