using System.Threading;
using Code.Core.AssetManagement;
using Code.Services.AudioService;
using Code.Services.ConfigData;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Code.Core.EntryPoints
{
    public class CoreLoader : IInitializable
    {
        private readonly IConfigProvider _configProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly AudioService _audioService;
   

        public CoreLoader(IConfigProvider configProvider, IAssetProvider assetProvider,
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
        }
        
    }
}
