using Code.ConfigData.Levels;
using Code.Logic.EnemySpawners;

namespace Code.Services
{
    public interface ILevelEventHandler : IService
    {
        LevelReward GetLevelReward();
        void MonsterSpawnerSlain(EnemySpawner spawner);
        void NextLevelReward(LevelReward levelReward);
        void InitCurrentLevel(int enemySpawnersCount);
    }
}