using Code.Data.PlayerData;
using Code.Services.RandomService;
using UnityEngine;

namespace Code.Entity.Enemy
{
    public class ExpDrop : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        
        private int _minExp;
        private int _maxExp;
        private PlayerExp _playerExp;

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
        }

        public void SetExperienceGain(int minExp, int maxExp)
        {
            _minExp = minExp;
            _maxExp = maxExp;
        }
    }
}
