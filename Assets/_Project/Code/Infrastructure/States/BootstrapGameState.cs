using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.SceneManagement;
using Code.Services;

namespace Code.Infrastructure.States
{
    public class BootstrapGameState : IGameState
    {
        private const string InitialScene = "Initialize";
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assetProvider;
        public IGameStateMachine GameStateMachine { get; set; }

        public BootstrapGameState(SceneLoader sceneLoader,
            IStaticDataService staticData, IAssetProvider assetProvider)
        {
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _assetProvider = assetProvider;
        }

        public void Enter()
        {
            _assetProvider.Initialize();
            _staticData.Load();
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