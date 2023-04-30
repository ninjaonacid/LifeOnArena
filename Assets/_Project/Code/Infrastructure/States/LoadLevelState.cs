using Code.Infrastructure.AssetManagment;
using Code.Infrastructure.Services;
using Code.Logic;
using Code.Services.AudioService;
using Code.Services.SaveLoad;
using Code.UI.Services;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly LoadingCurtain _curtain;
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
            LoadingCurtain curtain, IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _uiFactory = uiFactory;
            _audioService = audioService;
            _assetProvider = assetProvider;
            _saveLoadService = saveLoadService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
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

        //private async UniTask InitGameWorld()
        //{
        //    string sceneKey = SceneManager.GetActiveScene().name;

        //    LevelConfig levelConfig = _staticData.ForLevel(sceneKey);


        //}


    }
}