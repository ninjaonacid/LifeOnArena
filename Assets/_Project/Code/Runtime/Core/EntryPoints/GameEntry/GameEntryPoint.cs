using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Config;
using Code.Runtime.Services.LevelLoader;
using VContainer.Unity;

namespace Code.Runtime.Core.EntryPoints.GameEntry
{
    public class GameEntryPoint : IInitializable
    {
        private readonly InitializeGameState _gameState;
        private readonly LevelLoader _levelLoader;
        private readonly AudioService _audioService;
        private readonly ConfigProvider _config;

        private LevelIdentifier _startLevelId;
        public GameEntryPoint(InitializeGameState gameState,
            LevelIdentifier startLevelId,
            LevelLoader levelLoader, 
            AudioService audioService, 
            ConfigProvider config)
        {
            _gameState = gameState;
            _startLevelId = startLevelId;
            _levelLoader = levelLoader;
            _audioService = audioService;
            _config = config;
            
        }

        public void Initialize()
        {
            _gameState.LoadProgressOrInitNew();
            
            _levelLoader.LoadLevel(_startLevelId);
        }
        
    }
}