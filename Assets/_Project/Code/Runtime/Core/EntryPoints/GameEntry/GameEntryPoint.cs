using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Config;
using Code.Runtime.Modules.LocalizationProvider;
using Code.Runtime.Services.LevelLoaderService;
using VContainer.Unity;

namespace Code.Runtime.Core.EntryPoints.GameEntry
{
    public class GameEntryPoint : IInitializable
    {
        private readonly InitializeGameState _gameState;
        private readonly LevelLoader _levelLoader;
        private readonly ConfigProvider _config;
        private readonly LocalizationService _localizationService;
        
        private readonly LevelIdentifier _startLevelId;
        public GameEntryPoint(InitializeGameState gameState,
            LevelIdentifier startLevelId,
            LevelLoader levelLoader,
            ConfigProvider config
          )
        {
            _gameState = gameState;
            _startLevelId = startLevelId;
            _levelLoader = levelLoader;
            _config = config;
        }

        public void Initialize()
        {
            _gameState.LoadDataOrCreateNew();
            _levelLoader.LoadLevel(_startLevelId);
        }
        
    }
}