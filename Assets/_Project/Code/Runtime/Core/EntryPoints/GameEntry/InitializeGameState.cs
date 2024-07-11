using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Core.Config;
using Code.Runtime.Data;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI.Model.AbilityMenu;

namespace Code.Runtime.Core.EntryPoints.GameEntry
{
    public class InitializeGameState
    {
        private readonly ConfigProvider _configProvider;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameDataContainer _gameDataContainer;
        
        public InitializeGameState(
            ConfigProvider configProvider, 
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
                _saveLoadService.LoadPlayerData()
                ?? NewPlayerData();

            _gameDataContainer.AudioData = _saveLoadService.LoadAudioData() ?? NewAudioData();

        }

        private PlayerData NewPlayerData()
        {
            PlayerData data = new PlayerData("Shelter");

            data.TutorialData.IsTutorialCompleted = true;

            data.WorldData.LootData.Collected = 10000;

            StatDatabase characterStats = _configProvider.CharacterStats();
            
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

        private AudioData NewAudioData()
        {
            AudioData audioData = new AudioData
            {
                isMusicMuted = false,
                isSoundMuted = false
            };

            return audioData;
        }
    }
}
