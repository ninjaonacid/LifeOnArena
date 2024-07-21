using Code.Runtime.Data.PlayerData;
using Code.Runtime.Services;
using Code.Runtime.Services.RandomService;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class ExpDrop : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        
        private int _minExp;
        private int _maxExp;
        private PlayerExp _playerExp;
        private LevelCollectableTracker _collectableTracker;

        private IRandomService _random;
        public void Construct(IRandomService randomService, PlayerExp playerExp, LevelCollectableTracker collectableTracker)
        {
            _random = randomService;
            _playerExp = playerExp;
            _collectableTracker = collectableTracker;
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
