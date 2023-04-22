using System;
using Code.Logic.EnemySpawners;
using Code.StaticData.Levels;

namespace Code.Services
{
    public interface ILevelEventHandler : IService
    {
        event Action PlayerDead;
        event Action MonsterSpawnersCleared;
        LevelReward GetLevelReward();
        void MonsterSpawnerSlain(EnemySpawner spawner);
        void HeroDeath();
        void NextLevelReward(LevelReward levelReward);
        void InitCurrentLevel(int enemySpawnersCount);
    }
}