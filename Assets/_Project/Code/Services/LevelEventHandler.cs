using System;
using Code.Logic.EnemySpawners;
using Code.StaticData.Levels;

namespace Code.Services
{
    public class LevelEventHandler : ILevelEventHandler
    {

        public event Action MonsterSpawnersCleared;
        public event Action PlayerDead;

        private LevelReward _levelReward;

        private int _clearedSpawnersCount;
        private int _enemySpawners;


        public void InitCurrentLevel(int enemySpawnersCount)
        {
            _clearedSpawnersCount = 0;
            _enemySpawners = enemySpawnersCount;
 
        }

        public void NextLevelReward(LevelReward levelReward)
        {
            _levelReward = levelReward;
        }

        public void HeroDeath()
        {
            PlayerDead?.Invoke();
        }

        public void MonsterSpawnerSlain(EnemySpawnPoint spawner)
        {
            _clearedSpawnersCount++;

            if (_clearedSpawnersCount == _enemySpawners)
            {
                MonsterSpawnersCleared?.Invoke();
            }
        }

        public LevelReward GetLevelReward() => _levelReward;
    }
}
