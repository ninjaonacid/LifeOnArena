using System.Threading;
using Code.Core.AssetManagement;
using Code.Services.AudioService;
using Code.Services.ConfigData;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Code.Core.EntryPoints
{
    public class CoreEntryPoint : IAsyncStartable
    {
        private readonly IConfigProvider _configProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly AudioService _audioService;
   

        public CoreEntryPoint(IConfigProvider configProvider, IAssetProvider assetProvider,
            AudioService audioService)
        {
            _configProvider = configProvider;
            _assetProvider = assetProvider;
            _audioService = audioService;
        }

        public void Initialize()
        {
            _configProvider.Load();
            _assetProvider.Initialize();
            _audioService.InitializeAudio(_configProvider.GetLibrary()).Forget();
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _configProvider.Load();
            _assetProvider.Initialize();
            await _audioService.InitializeAudio(_configProvider.GetLibrary());
        }
    }
}
