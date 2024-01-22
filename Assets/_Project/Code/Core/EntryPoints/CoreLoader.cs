using Code.Core.AssetManagement;
using Code.Core.Factory;
using Code.Services.AudioService;
using Code.Services.ConfigData;
using UnityEngine;
using VContainer.Unity;

namespace Code.Core.EntryPoints
{
    public class CoreLoader : IInitializable
    {
        private readonly IConfigProvider _configProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly AudioService _audioService;
        private IFactory<GameObject> _factory;

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
            _audioService.InitializeAudio();
        }
        
    }
}
