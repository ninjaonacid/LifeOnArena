using System;
using Code.Logic.EnemySpawners;
using Code.StaticData.Levels;

namespace Code.Services
{
    public class LevelEventHandler : ILevelEventHandler
    {

        public event Action MonsterSpawnersCleared;
        public event Action PlayerDead;

        private LevelConfig _currentLevel;
        private LevelReward _levelReward;

        private int _clearedSpawnersCount;


        public void ResetSpawnerCounter()
        {
            _clearedSpawnersCount = 0;
        }
        public void HeroDeath()
        {
            PlayerDead?.Invoke();
        }

        public void MonsterSpawnerSlain(EnemySpawnPoint spawner)
        {
            _clearedSpawnersCount++;

            if (_clearedSpawnersCount == _currentLevel.EnemySpawners.Count)
            {
                MonsterSpawnersCleared?.Invoke();
            }
        }

        public void SetCurrentLevel(LevelConfig levelConfig)
        {
            _currentLevel = levelConfig;
        }

        public void SetLevelReward(LevelReward levelReward)
        {
            _levelReward = levelReward;
        }

        public LevelReward GetLevelReward() => _levelReward;
    }
}
