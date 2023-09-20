using Code.Core.SceneManagement;
using VContainer.Unity;

namespace Code.Core.EntryPoints.GameEntry
{
    public class GameEntryPoint : IInitializable
    {
        private readonly InitializeGameState _gameState;
        private readonly SceneLoader _sceneLoader;
        
        private const string MainMenuScene = "MainMenu";

        public GameEntryPoint(InitializeGameState gameState, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _gameState = gameState;
        }
        
        public void Initialize()
        {
            _gameState.LoadProgressOrInitNew();
            
            _sceneLoader.Load(MainMenuScene);
        }
    }
}