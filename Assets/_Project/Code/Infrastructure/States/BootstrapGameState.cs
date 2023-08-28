using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.SceneManagement;
using Code.Services;

namespace Code.Infrastructure.States
{
    public class BootstrapGameState : IGameState
    {
        private const string InitialScene = "Initialize";
        private readonly SceneLoader _sceneLoader;
        private readonly IConfigDataProvider _configData;
        private readonly IAssetProvider _assetProvider;
        public IGameStateMachine GameStateMachine { get; set; }

        public BootstrapGameState(SceneLoader sceneLoader,
            IConfigDataProvider configData, IAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _configData = configData;
            _assetProvider = assetProvider;
        }

        public void Enter()
        {
            _assetProvider.Initialize();
            _configData.Load();
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