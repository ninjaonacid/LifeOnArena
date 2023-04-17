
using Code.Infrastructure.Services;
using Code.Infrastructure.States;
using Code.Services;
using Code.StaticData.Levels;
using Code.UI.Services;
using UnityEngine;
using VContainer;

namespace Code.Logic.LevelTransition
{
    public class LevelTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private LevelRewardIcon _levelRewardIcon;
        [SerializeField] private LevelConfig _nextLevelData;
        [SerializeField] private LevelReward _levelReward;

        private IGameStateMachine _gameStateMachine;
        private ILevelEventHandler _levelEventHandler;
    
        private bool _isTriggered;

        [Inject]
        public void Construct(
            IGameStateMachine gameStateMachine,
            ILevelEventHandler levelEventHandler,
            IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _levelEventHandler = levelEventHandler;

       
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggered) return;

            if (other.CompareTag("Player"))
            {
                _levelEventHandler.NextLevelReward(_levelReward);
                _gameStateMachine.Enter<LoadLevelState, string>(_nextLevelData.LevelKey);
                _isTriggered = true;
            }
        }


        private void ShowRewardIcon()
        {
            _levelRewardIcon.ShowSprite();

        }

        private void OnDestroy()
        {
            _levelEventHandler.MonsterSpawnersCleared -= ShowRewardIcon;
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
