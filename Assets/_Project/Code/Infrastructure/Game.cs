using Code.Infrastructure.States;
using UnityEngine;

namespace Code.Infrastructure
{
    public class Game
    {
        private readonly IGameStateMachine _stateMachine;

        
        public Game(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void StartGame()
        {
            Debug.Log("GameStarted");
            _stateMachine.Enter<BootstrapState>();
        }
    }
}