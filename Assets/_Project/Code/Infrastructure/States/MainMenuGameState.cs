using Code.Infrastructure.SceneManagement;
using Code.Services.PersistentProgress;
using Code.UI.Buttons;
using Code.UI.Services;

namespace Code.Infrastructure.States
{
    public class MainMenuGameState : IGameState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _screen;

        private readonly IWindowService _windowService;
        private readonly IUIFactory _uiFactory;
        private readonly IProgressService _progress;
        public IGameStateMachine GameStateMachine { get; set; }

        public MainMenuGameState(IUIFactory uiFactory,
            IWindowService windowService,
            IProgressService progress,
            SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _windowService = windowService;
            _progress = progress;
        }

        public void Exit()
        {
        }
        
        public void Enter()
        {
            _sceneLoader.Load("MainMenu", InitMainMenu);
        }

        public void InitMainMenu()
        {
            //_uiFactory.CreateCore();
           // var menu = _uiFactory.CreateSelectionMenu(_windowService);
           // menu.GetComponentInChildren<StartGameButton>().Button.onClick.AddListener(EnterLoadLevelState);
        }

        private void EnterLoadLevelState()
        {
            GameStateMachine.Enter<LoadLevelState,string>("StoneDungeon_Arena_1");
        }
    }
}
