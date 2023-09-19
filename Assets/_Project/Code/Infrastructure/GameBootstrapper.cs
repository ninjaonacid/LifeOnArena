using Code.Infrastructure.SceneManagement;
using Code.Services.ConfigData;
using VContainer.Unity;

namespace Code.Infrastructure
{
    public class GameBootstrapper : IInitializable
    {
        private Game _game;
        private readonly SceneLoader _sceneLoader;
        private ConfigProvider _configProvider;
        private const string MainMenuScene = "MainMenu";

        public GameBootstrapper(SceneLoader sceneLoader, ConfigProvider configProvider)
        {
            _sceneLoader = sceneLoader;
            _configProvider = configProvider;
        }
        
        public void Initialize()
        {
            _game = new Game();
            _game.StartGame();
            _configProvider.Load();
            _sceneLoader.Load("MainMenu");
        }
    }
}