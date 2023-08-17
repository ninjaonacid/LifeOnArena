using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData.StatSystem;

namespace Code.Infrastructure.States
{
    public class LoadProgressGameState : IGameState
    {
        private readonly IGameDataService _gameDataService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticData;

        public IGameStateMachine GameStateMachine { get; set; }

        public LoadProgressGameState(
            IGameDataService gameDataService,
            ISaveLoadService saveLoadService,
            IStaticDataService staticData)
        {
          
            _gameDataService = gameDataService;
            _saveLoadService = saveLoadService;
            _staticData = staticData;
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
            _gameDataService.Progress =
                _saveLoadService.LoadProgress()
                ?? NewProgress();
        }


        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress("Shelter");

            progress.WorldData.LootData.Collected = 100;

            StatDatabase characterStats = _staticData.ForCharacterStats();

            foreach (StatDefinition stat in characterStats.PrimaryStats)
            {
                progress.CharacterStatsData.StatsData.TryAdd(stat.name, stat.BaseValue);
            }

            foreach (StatDefinition stat in characterStats.StatDefinitions)
            {
                progress.CharacterStatsData.StatsData.TryAdd(stat.name, stat.BaseValue);
            }

            foreach (StatDefinition stat in characterStats.AttributeDefinitions)
            {
                progress.CharacterStatsData.StatsData.TryAdd(stat.name, stat.BaseValue);
            }
            
            return progress;
        }
    }
}