using System;
using Code.Logic.EnemySpawners;

namespace Code.Services
{
    public class GameEventHandler : IGameEventHandler
    {

        public event Action MonsterSpawnersCleared;
        public event Action PlayerDead;
        public int LevelSpawnersCount { get; set; }
        public int ClearedSpawnersCount { get; set; }

        public void ResetClearedCounter()
        {
            ClearedSpawnersCount = 0;
        }
        public void HeroDeath()
        {
            PlayerDead?.Invoke();
        }
        public void MonsterSpawnerSlain(SpawnPoint spawner)
        {
            ClearedSpawnersCount++;

            if (ClearedSpawnersCount == LevelSpawnersCount)
            {
                MonsterSpawnersCleared?.Invoke();
            }
        }

    }
}
