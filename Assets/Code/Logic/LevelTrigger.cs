using Code.Infrastructure.Services;
using Code.Infrastructure.States;
using Code.Services;
using Code.StaticData.Levels;
using UnityEngine;

namespace Code.Logic
{
    public class LevelTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
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

        private void OnDrawGizmos()
        {
            if (!_collider) return;

            Gizmos.color = new Color32(150, 0, 0, 150);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}
