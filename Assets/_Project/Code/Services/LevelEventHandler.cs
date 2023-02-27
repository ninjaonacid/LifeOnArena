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

      
    }
}
