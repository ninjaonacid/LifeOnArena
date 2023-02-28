using Code.Infrastructure.Services;
using Code.Infrastructure.States;
using Code.Services.LevelTransitionService;
using Code.StaticData.Levels;
using Code.UI.Services;
using UnityEngine;

namespace Code.Logic.LevelTransition
{
    public class LevelTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private LevelRewardIcon _levelRewardIcon;
        [SerializeField] private LevelConfig _nextLevelData;

        private IGameStateMachine _gameStateMachine;
        private ILevelTransitionService _levelTransitionService;
        private IUIFactory _uIFactory;
        private bool _isTriggered;

        public void Construct(IGameStateMachine gameStateMachine, ILevelTransitionService levelTransition, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _levelTransitionService = levelTransition;
            _uIFactory = uiFactory;;
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

        private async void InitializeNextLevel()
        {
            _nextLevelData = _levelTransitionService.GetNextLevel();
            
            //var sprite = await _uIFactory.CreateSprite(_nextLevelData.LevelReward);
            //_levelRewardIcon.GetComponent<SpriteRenderer>().sprite = sprite;
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
