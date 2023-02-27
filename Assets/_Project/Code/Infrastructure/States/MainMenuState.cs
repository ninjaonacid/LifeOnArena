using Code.Logic;
using Code.Services;
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

        private readonly ServiceLocator _services;
        private IWindowService _windowService;
        private IUIFactory _uiFactory;
        private IProgressService _progress;
        public MainMenuState(GameStateMachine gameStateMachine, 
            SceneLoader sceneLoader, 
            ServiceLocator services,
            LoadingCurtain curtain)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _curtain = curtain;
            _services = services;
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
            _progress = _services.Single<IProgressService>();
            _windowService = _services.Single<IWindowService>();
            _uiFactory = _services.Single<IUIFactory>();

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
            _gameStateMachine.Enter<LoadLevelState,string>(_progress.Progress.WorldData.PositionOnLevel.Level);
        }
    }
}
