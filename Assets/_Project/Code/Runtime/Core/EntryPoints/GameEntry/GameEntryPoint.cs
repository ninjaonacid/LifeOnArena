using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.LevelLoaderService;
using Code.Runtime.Modules.LocalizationProvider;
using GamePush;
using VContainer.Unity;

namespace Code.Runtime.Core.EntryPoints.GameEntry
{
    public class GameEntryPoint : IInitializable
    {
        private readonly GameInitializer _gameInitializer;
        private readonly LevelLoader _levelLoader;
        private readonly LocalizationService _localizationService;
        private readonly AudioService _audioService;
        private readonly LevelIdentifier _startLevelId;
        public GameEntryPoint(GameInitializer gameInitializer,
            LevelIdentifier startLevelId,
            LevelLoader levelLoader,
            AudioService audioService,
            LocalizationService localizationService
          )
        {
            _gameInitializer = gameInitializer;
            _startLevelId = startLevelId;
            _levelLoader = levelLoader;
            _audioService = audioService;
            _localizationService = localizationService;
        }

        public void Initialize()
        {
            _gameInitializer.LoadDataOrCreateNew();
            _audioService.InitializeAudio();
            
            if (GP_Init.isReady)
            {
                _localizationService.Initialize();
                _levelLoader.LoadLevel(_startLevelId);
            }
            else
            {
                GP_Init.OnReady += OnGamePushReady;
            }
        }

        private void OnGamePushReady()
        {
            _localizationService.Initialize();
            _levelLoader.LoadLevel(_startLevelId);
        }
    }
}