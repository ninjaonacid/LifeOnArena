using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.SceneManagement;

namespace Code.Runtime.Services.LevelLoaderService
{
    public class LevelLoader
    {
        private SceneLoader _sceneLoader;
        private ConfigProvider _configProvider;
        private LevelConfig _currentLevel;

        public LevelLoader(SceneLoader sceneLoader, ConfigProvider configProvider)
        {
            _sceneLoader = sceneLoader;
            _configProvider = configProvider;
        }
        
        public void LoadLevel(LevelIdentifier levelIdentifier)
        {
            var levelConfig = _configProvider.Level(levelIdentifier.Id);
            _currentLevel = levelConfig;
            _sceneLoader.Load(levelConfig.SceneKey);
        }
        
        public void LoadLevel(int levelId)
        {
            var levelConfig = _configProvider.Level(levelId);
            _currentLevel = levelConfig;
            _sceneLoader.Load(levelConfig.SceneKey);
        }

        public void LoadLevel(string levelId)
        {
            var levelConfig = _configProvider.Level(levelId);
            _currentLevel = levelConfig;
            _sceneLoader.Load(levelConfig.SceneKey);
        }
        
        public LevelConfig GetCurrentLevelConfig()
        {
            return _currentLevel;
        }
    }
}
