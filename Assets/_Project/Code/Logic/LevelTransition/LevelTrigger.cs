using Code.Infrastructure.Services;
using Code.Infrastructure.States;
using Code.Services.LevelTransitionService;
using Code.StaticData.Levels;
using UnityEngine;

namespace Code.Logic.LevelTransition
{
    public class LevelTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;

        private LevelConfig _nextLevelData;

        private IGameStateMachine _gameStateMachine;
        private ILevelTransitionService _levelTransitionService;
        private bool _isTriggered;

        public void Construct(IGameStateMachine gameStateMachine, ILevelTransitionService levelTransition)
        {
            _gameStateMachine = gameStateMachine;
            _levelTransitionService = levelTransition;
            InitializeNextLevel();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggered) return;

            if (other.CompareTag("Player"))
            {
                _gameStateMachine.Enter<LoadLevelState, string>(_nextLevelData.LevelKey);
                _isTriggered = true;
            }
        }

        private void InitializeNextLevel()
        {
            _nextLevelData = _levelTransitionService.GetNextLevel();
        }

        private void OnDrawGizmos()
        {
            if (!_collider) return;

            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = new Color32(150, 0, 0, 150);
            Gizmos.DrawCube(_collider.center, _collider.size);
        }
    }
}
