using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.Factory;
using UnityEngine;
using VContainer.Unity;

namespace Code.Runtime.Core.EntryPoints
{
    public class CoreLoader : IInitializable
    {
        private readonly ConfigProvider _configProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly Audio.AudioService _audioService;
        private IFactory<GameObject> _factory;

        public CoreLoader(ConfigProvider configProvider, IAssetProvider assetProvider,
            Audio.AudioService audioService)
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
