using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Logic.EnemySpawners;

namespace Code.Runtime.Services
{
    public interface ILevelEventHandler : IService
    {
        LevelReward GetLevelReward();
        void MonsterSpawnerSlain(EnemySpawner spawner);
        void NextLevelReward(LevelReward levelReward);
        void InitCurrentLevel(int enemySpawnersCount);
    }
}