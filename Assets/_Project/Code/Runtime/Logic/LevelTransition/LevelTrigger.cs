using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.EventSystem;
using Code.Runtime.UI.Services;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Logic.LevelTransition
{
    public class LevelTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private LevelRewardIcon _levelRewardIcon;
        [SerializeField] private LevelConfig _nextLevelData;
        [SerializeField] private LevelReward _levelReward;

        private IEventSystem _eventSystem;

        private bool _isTriggered;

        [Inject]
        public void Construct(
            IEventSystem eventSystem,
            IUIFactory uiFactory)
        {
            _eventSystem = eventSystem;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggered) return;

            if (other.CompareTag("Player"))
            {
               // _levelEventHandler.NextLevelReward(_levelReward);
                _isTriggered = true;
            }
        }
        
        private void ShowRewardIcon()
        {
            _levelRewardIcon.ShowSprite();

        }

        private void OnDestroy()
        {
           // _levelEventHandler.MonsterSpawnersCleared -= ShowRewardIcon;
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
