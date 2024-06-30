using System.Collections.Generic;
using System.Linq;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.Config;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.UI.Model.ArenaSelectionScreenModel
{
    public class ArenaSelectionScreenModel : IScreenModel
    {
        private ConfigProvider _configProvider;
        private IGameDataContainer _gameData;
        public List<LocationPointModel> LocationPointModel { get; private set; } = new();
        public ArenaSelectionScreenModel(ConfigProvider configProvider, IGameDataContainer gameData)
        {
            _configProvider = configProvider;
            _gameData = gameData;
        }

        public void Initialize()
        {
            var playableLevels = _configProvider
                .LoadLevels()
                .Where(x => x.LevelType == LevelType.Playable);

            foreach (var level in playableLevels)
            {
                var locationModel = new LocationPointModel()
                {
                    LocationName =  level.LocationName,
                    LevelId = level.LevelId.Id,
                    IsUnlocked = level.UnlockRequirement == null || level.UnlockRequirement.CheckRequirement(_gameData.PlayerData)
                };
                
                LocationPointModel.Add(locationModel);
            }
        }

        public bool IsLevelUnlocked(int id)
        {
            foreach (var model in LocationPointModel)
            {
                if (model.LevelId == id)
                {
                    return model.IsUnlocked;
                }
            }
            return false;
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