using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;

namespace Code.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine,
            IProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            /*_gameStateMachine.Enter<LoadLevelState, string>(
                _progressService.Progress.WorldData.PositionOnLevel.Level);*/
            _gameStateMachine.Enter<MainMenuState>();
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
            var progress = new PlayerProgress("Level1");

            progress.HeroHp.MaxHP = 50f;
            progress.CharacterStats.BaseDamage = 20f;
            progress.CharacterStats.BaseAttackRadius = 1f;
            progress.CharacterStats.BaseAttackSpeed = 1f;
            progress.SkillsData.SpinAttack.Damage = 20f;
            progress.HeroHp.ResetHP();

            return progress;
        }
    }
}