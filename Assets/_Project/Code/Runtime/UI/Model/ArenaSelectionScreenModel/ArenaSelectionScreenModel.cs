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
        public List<LocationPointModel> LevelModel { get; private set; } = new();

        public ArenaSelectionScreenModel(ConfigProvider configProvider)
        {
            _configProvider = configProvider;
            
        }

        public void Initialize()
        {
            var playableLevels = _configProvider
                .LoadLevels()
                .Where(x => x.LevelType == LevelType.Playable);

            foreach (var level in playableLevels)
            {
                LevelModel.Add(new LocationPointModel()
                {
                    LocationName =  level.LocationName,
                    LevelId = level.LevelId.Id
                });
            }
        }

        public bool IsLevelUnlocked(int index)
        {
            var levelModel = LevelModel[index];
            return levelModel.IsUnlocked;
        }
    }

    public class LocationPointModel
    {
        public Sprite Icon;
        public bool IsUnlocked;
        public string LocationName;
        public int LevelId;

    }
}