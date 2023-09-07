using Code.Logic.EnemySpawners;
using Code.StaticData.Levels;

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