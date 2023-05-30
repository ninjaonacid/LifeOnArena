using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;

namespace Code.Infrastructure.States
{
    public class LoadProgressGameState : IGameState
    {
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public IGameStateMachine GameStateMachine { get; set; }

        public LoadProgressGameState(
            IProgressService progressService,
            ISaveLoadService saveLoadService)
        {
          
            _progressService = progressService;
            _saveLoadService = saveLoadService;
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
            
            progress.CharacterStats.InitBaseStats(100, 10, 30, 3);
            progress.WorldData.LootData.Collected = 100;
            progress.CharacterStats.ResetHP();

            return progress;
        }
    }
}