using Code.Core.SceneManagement;
using Code.Services.AudioService;
using Code.Services.ConfigData;
using VContainer.Unity;

namespace Code.Core.EntryPoints.GameEntry
{
    public class GameEntryPoint : IInitializable
    {
        private readonly InitializeGameState _gameState;
        private readonly SceneLoader _sceneLoader;
        private readonly AudioService _audioService;
        private readonly IConfigProvider _config;
        
        private const string MainMenuScene = "MainMenu";


        public GameEntryPoint(InitializeGameState gameState, SceneLoader sceneLoader, AudioService audioService, IConfigProvider config)
        {
            _gameState = gameState;
            _sceneLoader = sceneLoader;
            _audioService = audioService;
            _config = config;
        }

        public void Initialize()
        {
            _gameState.LoadProgressOrInitNew();
            
            _sceneLoader.Load(MainMenuScene);
        }
        
    }
}