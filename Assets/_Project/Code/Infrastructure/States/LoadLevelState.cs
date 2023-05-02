using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.SceneManagement;
using Code.Services.AudioService;
using Code.Services.SaveLoad;
using Code.UI.Services;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAssetProvider _assetProvider;
        private readonly IAudioService _audioService;
        private readonly IUIFactory _uiFactory;
        public IGameStateMachine GameStateMachine { get; set; }

        public LoadLevelState(
            ISaveLoadService saveLoadService,
            IAssetProvider assetProvider,
            IAudioService audioService,
            SceneLoader sceneLoader, 
            IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _audioService = audioService;
            _assetProvider = assetProvider;
            _saveLoadService = saveLoadService;
        }

        public void Enter(string sceneName)
        {
            _saveLoadService.Cleanup();
            _assetProvider.Cleanup();
            _audioService.InitAssets();

            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InitUiCore();

            GameStateMachine.Enter<GameLoopState>();
        }

        private void InitUiCore()
        {
            _uiFactory.CreateCore();
        }

    }
}