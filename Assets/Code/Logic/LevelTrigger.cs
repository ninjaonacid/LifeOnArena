using Code.Infrastructure.Services;
using Code.Infrastructure.States;
using Code.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.Logic
{
    public class LevelTrigger : MonoBehaviour
    {
        public LevelStaticData NextLevelData;

        private IGameStateMachine _gameStateMachine;
        private bool _isTriggered;

        private void Awake()
        {
            _gameStateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggered) return;

            if (other.CompareTag("Player"))
            {
                _gameStateMachine.Enter<LoadLevelState, string>(NextLevelData.LevelKey);
                _isTriggered = true;
            }
        }
    }
}
