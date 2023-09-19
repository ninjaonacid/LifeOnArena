using Code.Core.EntryPoints.GameEntry;
using Code.Infrastructure;
using Code.Infrastructure.SceneManagement;
using Code.Services;
using VContainer.Unity;

namespace Code.Core
{
    public class GameEntryPoint : IInitializable
    {
        private Game _game;
        private GameStateInitialize _gameState;
        private readonly SceneLoader _sceneLoader;
        private const string MainMenuScene = "MainMenu";

        public GameEntryPoint(GameStateInitialize gameState, SceneLoader sceneLoader)
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