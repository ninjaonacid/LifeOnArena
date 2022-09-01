using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;

namespace Code.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine,
            IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState, string>(
                _progressService.Progress.WorldData.PositionOnLevel.Level);
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
            var progress = new PlayerProgress("MainScene");

            progress.heroHeroHp.MaxHP = 500f;
            progress.CharacterStats.BaseDamage = 0f;
            progress.CharacterStats.BaseAttackRadius = 1f;
            progress.CharacterStats.BaseAttackSpeed = 1f;
            progress.heroHeroHp.ResetHP();

            return progress;
        }
    }
}