using System.Collections.Generic;
using Code.StaticData.Levels;

namespace Code.Services.LevelTransitionService
{
    public interface ILevelTransitionService : IService
    {
        void Init(List<LevelConfig> allLevels, List<LevelReward> allRewards);
        void SetCurrentLevel(LevelConfig levelData);
        LevelConfig GetNextLevel();
        LevelReward GetReward();
    }
}
