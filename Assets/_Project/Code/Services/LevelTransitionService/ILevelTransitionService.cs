using System.Collections.Generic;
using Code.StaticData.Levels;

namespace Code.Services.LevelTransitionService
{
    public interface ILevelTransitionService : IService
    {
        void SetLevels(List<LevelConfig> allLevels);
        void SetCurrentLevel(LevelConfig levelData);
        LevelConfig GetNextLevel();
    }
}
