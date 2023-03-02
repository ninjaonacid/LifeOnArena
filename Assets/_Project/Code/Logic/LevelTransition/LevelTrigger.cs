using System;
using Code.Infrastructure.Services;
using Code.Infrastructure.States;
using Code.Services;
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
        [SerializeField] private LevelReward _levelReward;

        private IGameStateMachine _gameStateMachine;
        private ILevelTransitionService _levelTransitionService;
        private ILevelEventHandler _levelEventHandler;
        private IUIFactory _uIFactory;
        private bool _isTriggered;

        public void Construct(
            IGameStateMachine gameStateMachine, 
            ILevelTransitionService levelTransition, 
            ILevelEventHandler levelEventHandler,
            IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _levelTransitionService = levelTransition;
            _uIFactory = uiFactory;
            _levelEventHandler = levelEventHandler;

            _levelEventHandler.MonsterSpawnersCleared += ShowRewardIcon;
            InitializeNextLevel();
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

        private async void InitializeNextLevel()
        {
            _nextLevelData = _levelTransitionService.GetNextLevel();
            _levelReward = _levelTransitionService.GetReward();

            var sprite = await _uIFactory.CreateSprite(_levelReward.SpriteReference);
            _levelRewardIcon.GetComponent<SpriteRenderer>().sprite = sprite;
            _levelRewardIcon.HideSprite();
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
