using Code.Runtime.ConfigData;
using Code.Runtime.Core.Config;
using Code.Runtime.Data;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Services.PersistentProgress;
using Code.Runtime.Services.SaveLoad;

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

            data.WorldData.LootData.Collected = 10000;

            StatDatabase characterStats = config.CharacterStats;
            
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

            data.HeroEquipment.WeaponIntId = config.StartWeapon.Id;
            
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
