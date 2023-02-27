using System;
using Code.Logic.EnemySpawners;
using Code.StaticData.Levels;

namespace Code.Services
{
    public interface ILevelEventHandler : IService
    {
        event Action PlayerDead;
        event Action MonsterSpawnersCleared;
        void MonsterSpawnerSlain(EnemySpawnPoint spawner);
        void HeroDeath();

        void ResetSpawnerCounter();
        void SetCurrentLevel(LevelConfig levelData);
       
    }
}