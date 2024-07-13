using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Config;
using Code.Runtime.Modules.LocalizationProvider;
using Code.Runtime.Modules.Utils;
using Code.Runtime.Services.LevelLoaderService;
using InstantGamesBridge;
using VContainer.Unity;

namespace Code.Runtime.Core.EntryPoints.GameEntry
{
    public class GameEntryPoint : IInitializable
    {
        private readonly InitializeGameState _gameState;
        private readonly LevelLoader _levelLoader;
        private readonly AudioService _audioService;
        private readonly ConfigProvider _config;
        private readonly LocalizationService _localizationService;
        
        private LevelIdentifier _startLevelId;
        public GameEntryPoint(InitializeGameState gameState,
            LevelIdentifier startLevelId,
            LevelLoader levelLoader, 
            AudioService audioService, 
            ConfigProvider config,
            LocalizationService localizationService)
        {
            _gameState = gameState;
            _startLevelId = startLevelId;
            _levelLoader = levelLoader;
            _audioService = audioService;
            _config = config;
            _localizationService = localizationService;
        }

        public void Initialize()
        {
            _gameState.LoadDataOrCreateNew();
            _levelLoader.LoadLevel(_startLevelId);

            if (WebApplication.IsWebApp)
            {
                _localizationService.Initialized += () => _localizationService.ChangeLanguage(Bridge.platform.language);
            }
        }
        
    }
}