using Code.Infrastructure.AssetManagment;
using Code.Infrastructure.Services;
using Code.Services;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initialize";
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assetProvider;
        public IGameStateMachine GameStateMachine { get; set; }

        public BootstrapState(SceneLoader sceneLoader,
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
            GameStateMachine.Enter<LoadProgressState>();
        }

    }
}