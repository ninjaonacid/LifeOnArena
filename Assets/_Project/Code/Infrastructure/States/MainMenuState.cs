using Code.Infrastructure.Services;
using Code.Logic;
using Code.Services.PersistentProgress;
using Code.UI.Buttons;
using Code.UI.Services;

namespace Code.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        private IWindowService _windowService;
        private IUIFactory _uiFactory;
        private IProgressService _progress;
        public IGameStateMachine GameStateMachine { get; set; }

        public MainMenuState(IUIFactory uiFactory,
            IWindowService windowService,
            IProgressService progress,
            SceneLoader sceneLoader,
            LoadingCurtain curtain)
        {
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _uiFactory = uiFactory;
            _windowService = windowService;
            _progress = progress;
        }

        public void Exit()
        {
            ResetPlayerHp();
        }

        private void ResetPlayerHp()
        {
            _progress.Progress.CharacterStats.ResetHP();
        }

        public void Enter()
        {
            _curtain.Show();
            _sceneLoader.Load("MainMenu", InitMainMenu);
        }

        public void InitMainMenu()
        {
            _uiFactory.CreateCore();
           var menu = _uiFactory.CreateSelectionMenu(_windowService);
           menu.GetComponentInChildren<StartGameButton>().Button.onClick.AddListener(EnterLoadLevelState);
           _curtain.Hide();
        }

        private void EnterLoadLevelState()
        {
            GameStateMachine.Enter<LoadLevelState,string>("StoneDungeon_1");
        }
    }
}
