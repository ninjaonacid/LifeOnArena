using Code.Runtime.ConfigData;
using Code.Runtime.Core.Config;
using Code.Runtime.Data;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.Runtime;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;

namespace Code.Runtime.Core.EntryPoints.GameEntry
{
    public class GameInitializer
    {
        private readonly ConfigProvider _configProvider;
        private readonly SaveLoadService _saveLoadService;
        private readonly IGameDataContainer _gameDataContainer;
        public GameInitializer(
            ConfigProvider configProvider, 
            IGameDataContainer dataContainer,
            SaveLoadService saveLoad)
        {
            _configProvider = configProvider;
            _saveLoadService = saveLoad;
            _gameDataContainer = dataContainer;
        }

        public void LoadDataOrCreateNew()
        {
            _gameDataContainer.PlayerData =
                _saveLoadService.LoadPlayerData()
                ?? NewPlayerData();

            _gameDataContainer.AudioData = _saveLoadService.LoadAudioData() ?? NewAudioData();

        }

        private PlayerData NewPlayerData()
        {
            InitialGameConfig config = _configProvider.GetInitialConfig();
            PlayerData data = new PlayerData();

            data.TutorialData.IsTutorialCompleted = !config.IsNeedTutorial;

            data.WorldData.LootData.CollectedLoot.Value = config.StartSouls;

            StatDatabase characterStats = config.CharacterStats;
            
            foreach (StatDefinition stat in characterStats.PrimaryStats)
            {
                data.StatsData.StatsValues.TryAdd(stat.name, stat.BaseValue);
                data.StatsData.StatsCapacities.TryAdd(stat.name, stat.Capacity);
                data.StatsData.StatPerLevel.TryAdd(stat.name, stat.StatPerLevel);
            }

            foreach (StatDefinition stat in characterStats.StatDefinitions)
            {
                data.StatsData.StatsValues.TryAdd(stat.name, stat.BaseValue);
                data.StatsData.StatsCapacities.TryAdd(stat.name, stat.Capacity);
                data.StatsData.StatPerLevel.TryAdd(stat.name, stat.StatPerLevel);
            }

            foreach (StatDefinition stat in characterStats.AttributeDefinitions)
            {
                data.StatsData.StatsValues.TryAdd(stat.name, stat.BaseValue);
                data.StatsData.StatsCapacities.TryAdd(stat.name, stat.Capacity);
                data.StatsData.StatPerLevel.TryAdd(stat.name, stat.StatPerLevel);
            }

            data.HeroEquipment.WeaponIntId = config.StartWeapon.Id;

            data.StatsData.StatUpgradePrice = config.StatUpgradePrice;

            data.PlayerExp.ExponentialFactor = config.ExperienceExponentialFactor;
            
            return data;
        }

        private AudioData NewAudioData()
        {
            AudioData audioData = new AudioData
            {
                isMusicOn = true,
                isSoundOn = true
            };

            return audioData;
        }
    }
}
