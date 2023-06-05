using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;

namespace Code.Infrastructure.States
{
    public class LoadProgressGameState : IGameState
    {
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticData;

        public IGameStateMachine GameStateMachine { get; set; }

        public LoadProgressGameState(
            IProgressService progressService,
            ISaveLoadService saveLoadService,
            IStaticDataService staticData)
        {
          
            _progressService = progressService;
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
            _progressService.Progress =
                _saveLoadService.LoadProgress()
                ?? NewProgress();
        }


        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress("Shelter");

            progress.WorldData.LootData.Collected = 100;

            //var characterStats = _staticData.ForCharacterStats();
            
            //stats initialization
            
            return progress;
        }
    }
}