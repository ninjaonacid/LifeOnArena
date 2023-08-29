using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData.StatSystem;

namespace Code.Infrastructure.States
{
    public class LoadProgressGameState : IGameState
    {
        private readonly IGameDataContainer _gameDataContainer;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IConfigDataProvider _configData;

        public IGameStateMachine GameStateMachine { get; set; }

        public LoadProgressGameState(
            IGameDataContainer gameDataContainer,
            ISaveLoadService saveLoadService,
            IConfigDataProvider configData)
        {
          
            _gameDataContainer = gameDataContainer;
            _saveLoadService = saveLoadService;
            _configData = configData;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            /*_gameStateMachine.Enter<LoadLevelState, string>(
                _progressService.Progress.WorldData.PositionOnLevel.Level);*/
            GameStateMachine.Enter<MainMenuGameState>();
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _gameDataContainer.PlayerData =
                _saveLoadService.LoadProgress()
                ?? NewProgress();
        }


        private PlayerData NewProgress()
        {
            PlayerData data = new PlayerData("Shelter");

            data.WorldData.LootData.Collected = 100;

            StatDatabase characterStats = _configData.ForCharacterStats();

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