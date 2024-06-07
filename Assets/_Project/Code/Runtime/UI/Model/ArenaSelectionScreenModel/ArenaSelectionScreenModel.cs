using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.Config;
using UnityEngine;

namespace Code.Runtime.UI.Model.ArenaSelectionScreenModel
{
    public class ArenaSelectionScreenModel : IScreenModel
    {
        private ConfigProvider _configProvider;
        public List<LevelModel> LevelModel { get; private set; } = new();

        public ArenaSelectionScreenModel(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
            
        }

        public void Initialize()
        {
            var playableLevels = _configProvider
                .LoadLevels()
                .Where(x => x.LevelType == LevelType.Playable)
                .OrderBy(x => x.RequiredLevel);

            foreach (var level in playableLevels)
            {
                LevelModel.Add(new LevelModel(level));
            }
        }

        public bool IsLevelUnlocked(int index)
        {
            var levelModel = LevelModel[index];
            return levelModel.IsUnlocked;
        }
    }

    public class LevelModel
    {
        public Sprite Icon;
        public int RequiredLevel;
        public bool IsUnlocked;

        public LevelConfig LevelConfig;
        public LevelModel(LevelConfig config)
        {
            LevelConfig = config;
        }
    }
}