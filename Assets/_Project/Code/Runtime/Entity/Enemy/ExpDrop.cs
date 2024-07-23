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
        
        [Inject] private LevelCollectableTracker _collectableTracker;

        private PlayerExp _playerExp;
        private RandomService _random;

        public void Construct(RandomService randomService, PlayerExp playerExp)
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
            var minExpResult = _minExp * ((float)_playerExp.Level / 2);

            var maxExpResult = _maxExp * ((float)_playerExp.Level / 2);

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