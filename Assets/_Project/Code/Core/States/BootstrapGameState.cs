using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.SceneManagement;
using Code.Services;

namespace Code.Infrastructure.States
{
    public class BootstrapGameState : IGameState
    {
        private const string InitialScene = "Initialize";
        private readonly SceneLoader _sceneLoader;
        private readonly IConfigProvider _config;
        private readonly IAssetProvider _assetProvider;
        public IGameStateMachine GameStateMachine { get; set; }

        public BootstrapGameState(SceneLoader sceneLoader,
            IConfigProvider config, IAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _config = config;
            _assetProvider = assetProvider;
        }

        public void Enter()
        {
            _assetProvider.Initialize();
            _config.Load();
            _sceneLoader.Load(InitialScene, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            GameStateMachine.Enter<LoadProgressGameState>();
        }

    }
}