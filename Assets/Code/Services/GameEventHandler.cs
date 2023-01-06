using System;
using Code.Logic.EnemySpawners;
using Code.StaticData;
using Code.StaticData.Levels;

namespace Code.Services
{
    public class GameEventHandler : IGameEventHandler
    {

        public event Action MonsterSpawnersCleared;
        public event Action PlayerDead;


        public int LevelSpawnersCount { get; set; }
        public int ClearedSpawnersCount { get; set; }

        public void ResetSpawnerCounter()
        {
            ClearedSpawnersCount = 0;
        }
        public void HeroDeath()
        {
            PlayerDead?.Invoke();
        }

        public void MonsterSpawnerSlain(EnemySpawnPoint spawner)
        {
            ClearedSpawnersCount++;

            if (ClearedSpawnersCount == LevelSpawnersCount)
            {
                MonsterSpawnersCleared?.Invoke();
            }
        }

        public void SetLevelSpawnerCount(LevelStaticData levelData)
        {
            LevelSpawnersCount = levelData.EnemySpawners.Count;
        }
    }
}
