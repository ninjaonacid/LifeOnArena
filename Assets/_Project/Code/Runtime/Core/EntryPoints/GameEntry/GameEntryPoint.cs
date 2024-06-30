using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.SceneManagement;
using VContainer.Unity;

namespace Code.Runtime.Core.EntryPoints.GameEntry
{
    public class GameEntryPoint : IInitializable
    {
        private readonly InitializeGameState _gameState;
        private readonly SceneLoader _sceneLoader;
        private readonly AudioService _audioService;
        private readonly ConfigProvider _config;
        
        private const string MainMenuScene = "MainMenu";


        public GameEntryPoint(InitializeGameState gameState, SceneLoader sceneLoader, AudioService audioService, ConfigProvider config)
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