using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services;
using Code.Runtime.Services.RandomService;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Enemy
{
    public class ExpDrop : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;

        private int _minExp;
        private int _maxExp;
        private PlayerExp _playerExp;
        
        [Inject] private LevelCollectableTracker _collectableTracker;

        private IRandomService _random;

        public void Construct(IRandomService randomService, PlayerExp playerExp)
        {
            _random = randomService;
            _playerExp = playerExp;
        }

        private void Start()
        {
            _enemyDeath.Happened += AddExperienceToPlayer;
        }

        private void AddExperienceToPlayer()
        {
            var minExpResult = _minExp * _playerExp.Level;

            var maxExpResult = _maxExp * _playerExp.Level;

            var randomExpResult = _random.RandomizeValue(minExpResult, maxExpResult);

            _playerExp.AddExperience(randomExpResult);

            _collectableTracker.CollectExperience(randomExpResult);
        }

        public void SetExperienceGain(int minExp, int maxExp)
        {
            _minExp = minExp;
            _maxExp = maxExp;
        }
    }
}