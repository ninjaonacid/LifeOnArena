using Code.Data;
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
        private HeroExp _heroExp;

        private IRandomService _random;
        public void Construct(IRandomService randomService, HeroExp heroExp)
        {
            _random = randomService;
            _heroExp = heroExp;
        }

        private void Start()
        {
            _enemyDeath.Happened += AddExperienceToPlayer;
        }

        private void AddExperienceToPlayer()
        {
            var minExpResult = _minExp * _heroExp.Level;

            var maxExpResult = _maxExp * _heroExp.Level;

            var randomExpResult = _random.RandomizeValue(minExpResult, maxExpResult);
            
            _heroExp.AddExperience(randomExpResult);
        }

        public void SetExperienceGain(int minExp, int maxExp)
        {
            _minExp = minExp;
            _maxExp = maxExp;
        }
    }
}
