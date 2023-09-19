using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData.StatSystem;
using UnityEngine;

namespace Code.Core.EntryPoints.GameEntry
{
    public class GameStateInitialize
    {
        private readonly IConfigProvider _configProvider;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameDataContainer _gameDataContainer;
        
        public GameStateInitialize(
            IConfigProvider configProvider, 
            IGameDataContainer dataContainer,
            ISaveLoadService saveLoad)
        {
            _configProvider = configProvider;
            _saveLoadService = saveLoad;
            _gameDataContainer = dataContainer;
        } 
        
        
        public void LoadProgressOrInitNew()
        {
            _gameDataContainer.PlayerData =
                _saveLoadService.LoadProgress()
                ?? NewProgress();
        }


        private PlayerData NewProgress()
        {
            PlayerData data = new PlayerData("Shelter");

            data.WorldData.LootData.Collected = 100;

            StatDatabase characterStats = _configProvider.ForCharacterStats();

            foreach (StatDefinition stat in characterStats.PrimaryStats)
            {
                data.StatsData.Stats.TryAdd(stat.name, stat.BaseValue);
            }

            foreach (StatDefinition stat in characterStats.StatDefinitions)
            {
                data.StatsData.Stats.TryAdd(stat.name, stat.BaseValue);
            }

            foreach (StatDefinition stat in characterStats.AttributeDefinitions)
            {
                data.StatsData.Stats.TryAdd(stat.name, stat.BaseValue);
            }
            
            return data;
        }
    }
}
