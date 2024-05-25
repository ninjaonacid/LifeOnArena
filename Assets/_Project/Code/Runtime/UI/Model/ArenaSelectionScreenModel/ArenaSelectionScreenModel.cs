using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.ConfigProvider;
using UnityEngine;

namespace Code.Runtime.UI.Model.ArenaSelectionScreenModel
{
    public class ArenaSelectionScreenModel : IScreenModel
    {
        private IConfigProvider _configProvider;
        public List<LevelModel> LevelModel { get; private set; } = new();

        public ArenaSelectionScreenModel(IConfigProvider configProvider)
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